using Francis.Database.Entities;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class StartAnonymousAnswer : TelegramAnswer
    {
        public override bool Public => true;

        public override bool CanProcess => true;

        public override int Priority => -1;


        public StartAnonymousAnswer(MessageAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            if (Context.User == null)
            {
                Context.User = new BotUser { TelegramId = Context.Message.Chat.Id };
                Context.Database.Add(Context.User);
                Context.Database.SaveChanges();
            }

            var text = $"Hello! I'm Francis, and I will help you to request medias. 😊\nI don't know you yet, can you click on the button to authenticate?";
            var message = await Context.Bot.SendMessage(Context.Message.Chat, text);

            await Context.Bot.EditMessage(message, text, replies: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData(
                text: "Send access request",
                callbackData: $"/request_access"
            )));

            Context.Logger.LogInformation($"Telegram user '{Context.Message.Chat.Username}' ({Context.Message.Chat.FirstName} {Context.Message.Chat.LastName}) started a new session");
        }
    }
}
