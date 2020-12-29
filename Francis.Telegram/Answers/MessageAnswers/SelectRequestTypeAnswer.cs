using Francis.Database.Entities;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class SelectRequestTypeAnswer : TelegramAnswer
    {
        public override bool CanProcess => true;

        public override int Priority => -1;


        public SelectRequestTypeAnswer(MessageAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            var progression = new RequestProgression { BotUserId = Context.Message.Chat.Id, Search = Context.Message.Text };
            Context.User.Progressions.Add(progression);

            Context.Database.SaveChanges();

            await Context.Bot.SendMessage(Context.Message.Chat, "What kind of media are you looking for?", new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData($"{RequestType.Movie}", $"/next_{RequestType.Movie} {progression.Id}"),
                    InlineKeyboardButton.WithCallbackData($"{RequestType.TvShow}", $"/next_{RequestType.TvShow} {progression.Id}"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Cancel request", $"/cancel {progression.Id}"),
                }
            }));

            Context.Logger.LogInformation($"User {await Context.GetName()} initiated search with '{progression.Search}'.");
        }
    }
}
