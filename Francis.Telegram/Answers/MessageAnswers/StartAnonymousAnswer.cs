using Francis.Database.Entities;
using Francis.Telegram.Answers;
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


        public StartAnonymousAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            if (Context.User == null)
            {
                Context.User = new BotUser { TelegramId = Context.Message.Chat.Id };
                Context.Database.Add(Context.User);
                Context.Database.SaveChanges();
            }

            if (Context.User.TelegramId == Context.Options.Value.AdminChat)
            {
                Context.User.Authorized = true;

                var adminText = $"Hello! I'm Francis, and I will help you to request medias. 😊\nWelcome back, Mr. the administrator!";
                await Context.Bot.SendMessage(Context.Message.Chat, adminText);

                return;
            }

            var text = $"Hello! I'm Francis, and I will help you to request medias. 😊\nI don't know you yet, can you click on the button to authenticate?";
            await Context.Bot.SendMessage(Context.Message.Chat, text, new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData(
                text: "Send access request",
                callbackData: $"/request_access"
            )));

            Context.Logger.LogInformation($"Telegram user {await Context.GetName()} started a new session");
        }
    }
}
