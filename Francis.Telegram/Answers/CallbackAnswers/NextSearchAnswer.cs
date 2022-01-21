using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Notification;
using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class NextSearchAnswer : TelegramAnswer<RequestProgression>
    {
        public override bool CanProcess => true;

        public override int Priority => -1;


        public NextSearchAnswer(AnswerContext<RequestProgression> context) : base(context)
        { }


        public override async Task Execute()
        {
            if (Context.Command != "/next")
            {
                var progression = new RequestProgression { BotUserId = Context.Message.Chat.Id, Search = Context.Message.Text };
                Context.User.Progressions.Add(progression);
                Context.Database.SaveChanges();
                Context.Parameters = new[] { progression.Id.ToString() };
            }

            var item = (await Context.Ombi.SearchMulti(Context.Progression.Search)).FirstOrDefault(x => !Context.Progression.ExcludedIds.Contains(x.Id));
            if (item == null)
            {
                Context.Progression.Status = RequestStatus.Error;

                var message = "I'm sorry, I could not find anything that matches your search... Are you sure you typed the name correctly?";
                if (Context.Message.From.Id == Context.User.TelegramId) 
                {
                    await Context.Bot.SendMessage(Context.User.TelegramId, message);
                }
                else 
                {
                    await Context.Bot.EditMessage(Context.Message, message);
                }

                Context.Logger.LogError($"User {await Context.GetName()} could not find any suitable media that matches '{Context.Progression.Search}'.");
                return;
            }

            Context.Progression.ExcludedIds.Add(item.Id);

            await Context.Bot.EditImage(Context.Message, item.Poster, "Is this what you are looking for?", (RequestItem)item, new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Exactly!", $"/chose_{item.MediaType} {Context.Progression.Id} {item.Id}"),
                    InlineKeyboardButton.WithCallbackData("Next...", $"/next {Context.Progression.Id}"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Cancel request", $"/cancel {Context.Progression.Id}"),
                }
            }));

            Context.Logger.LogInformation($"User {await Context.GetName()} continued searching with '{Context.Progression.Search}'. Result found: {item.Title} ({item.MediaType})");
        }
    }
}
