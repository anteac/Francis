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
                Context.User = new BotUser { Id = Context.Message.Chat.Id };
                Context.Database.Add(Context.User);
            }

            await Context.Bot.SendMessage(Context.Message.Chat, $"Hello ! I'm Francis, and I will help you to request medias. 😊\nI don't know you yet, can you click on the button to authenticate?",
                new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                    text: "Authenticate",
                    url: $"{Context.Options.Value.BaseUrl}/auth?clientId={Context.User.Id}"
                ))
            );

            Context.Logger.LogInformation($"Telegram user '{Context.Message.From.Username}' ({Context.Message.From.FirstName} {Context.Message.From.LastName}) started a new session");
        }
    }
}
