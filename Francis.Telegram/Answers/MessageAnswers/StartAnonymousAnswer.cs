using Francis.Database.Entities;
using Francis.Telegram.Contexts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class StartAnonymousAnswer : TelegramAnswer
    {
        internal override bool Public => true;

        internal override bool CanProcess => true;

        internal override int Priority => -1;


        public StartAnonymousAnswer(MessageAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            if (Context.User == null)
            {
                Context.User = new BotUser
                {
                    Id = Context.Message.Chat.Id,
                    ClientId = Guid.NewGuid().ToString().Replace("-", ""),
                };
                Context.Database.Add(Context.User);
            }

            await Context.Bot.Client.SendTextMessageAsync(
                chatId: Context.Message.Chat,
                text: $"Hello ! I'm Francis, and I will help you to request medias. 😊\nI don't know you yet, can you click on the button to authenticate?",
                replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                    text: "Authenticate",
                    url: $"{Context.Options.Value.BaseUrl}/auth?clientId={Context.User.ClientId}"
                ))
            );

            Context.Logger.LogInformation($"Telegram user '{Context.Message.From.Username}' ({Context.Message.From.FirstName} {Context.Message.From.LastName}) started a new session");
        }
    }
}
