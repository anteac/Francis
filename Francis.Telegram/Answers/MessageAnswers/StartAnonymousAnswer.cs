using Francis.Database.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class StartAnonymousAnswer : MessageAnswer
    {
        internal override bool Public => true;

        internal override bool CanProcess => true;


        public StartAnonymousAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            if (User == null)
            {
                User = new BotUser
                {
                    Id = Data.Chat.Id,
                    ClientId = Guid.NewGuid().ToString().Replace("-", ""),
                };
                Context.Add(User);
            }

            await Bot.Client.SendTextMessageAsync(
                chatId: Data.Chat,
                text: $"Hello ! I'm Francis, and I will help you to request medias. 😊\nI don't know you yet, can you click on the button to authenticate?",
                replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                    text: "Authenticate",
                    url: $"{Options.Value.BaseUrl}/auth?clientId={User.ClientId}"
                ))
            );

            Logger.LogInformation($"Telegram user '{Data.From.Username}' ({Data.From.FirstName} {Data.From.LastName}) started a new session");
        }
    }
}
