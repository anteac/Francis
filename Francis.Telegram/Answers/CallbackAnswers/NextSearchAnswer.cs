using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public abstract class NextSearchAnswer : TelegramAnswer<RequestProgression>
    {
        public NextSearchAnswer(CallbackAnswerContext<RequestProgression> context) : base(context)
        { }


        public override async Task Execute()
        {
            var item = (await GetItems()).FirstOrDefault(x => !Context.Progression.ExcludedIds.Contains(x.Id));
            if (item == null)
            {
                Context.Progression.Status = RequestStatus.Error;
                await Context.Bot.EditMessage(Context.Message, "I'm sorry, I could not find anything that matches your search... Are you sure you typed the name correctly?");
                Context.Logger.LogError($"User '{Context.User.Username}' could not find any suitable media that matches '{Context.Progression.Search}'.");
                return;
            }

            Context.Progression.ExcludedIds.Add(item.Id);

            await Context.Bot.EditImage(Context.Message, item.Image, "Is this what you are looking for?", item, new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Exactly!", $"/chose_{item.Type} {Context.Progression.Id} {item.Id}"),
                    InlineKeyboardButton.WithCallbackData("Next...", $"/next_{item.Type} {Context.Progression.Id}"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Cancel request", $"/cancel {Context.Progression.Id}"),
                }
            }));

            Context.Logger.LogInformation($"User '{Context.User.Username}' continued searching with '{Context.Progression.Search}'. Result found: {item.Title} ({item.Type} - {item.Year})");
        }


        protected abstract Task<RequestItem[]> GetItems();
    }
}
