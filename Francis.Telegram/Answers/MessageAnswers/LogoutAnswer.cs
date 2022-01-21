using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class LogoutAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == "/logout";


        public LogoutAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            var replies = new InlineKeyboardMarkup(new[] {
                InlineKeyboardButton.WithCallbackData("Confirm", "/confirm_logout"),
                InlineKeyboardButton.WithCallbackData("Cancel", "/cancel_logout"),
            });

            await Context.Bot.SendMessage(Context.Message.Chat, @"
You're about to logout. I'll miss you! 😔

Just a word on the consequences: I will delete everything I know about you, including the current media you're getting notifications of.
However, you can come back anytime you want! You would just have to tell me what you're watching again.

Can you confirm your choice? 🙂", replies);

            Context.Logger.LogInformation($"User {await Context.GetName()} initiated a logout process");
        }
    }
}
