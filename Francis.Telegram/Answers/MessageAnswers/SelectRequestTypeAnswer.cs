using Francis.Database.Entities;
using Francis.Models.Notification;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class SelectRequestTypeAnswer : MessageAnswer
    {
        internal override bool CanProcess => true;

        protected override bool HasProgression => true;


        public SelectRequestTypeAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            var progression = new RequestProgression { ChatId = Data.Chat.Id, Search = Data.Text };
            User.Progressions.Add(progression);

            Context.SaveChanges();

            await Bot.Client.SendTextMessageAsync(
                chatId: Data.Chat,
                text: "What kind of media are you looking for?",
                replyMarkup: new InlineKeyboardMarkup(new[]
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
                })
            );

            Logger.LogInformation($"User '{User.UserName}' initiated search with '{progression.Search}'.");
        }
    }
}
