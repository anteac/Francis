using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class CancelLogoutAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == $"/cancel_logout";


        public CancelLogoutAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            await Context.Bot.EditMessage(Context.Message, "Glad you decided to stay with me! 😊");
        }
    }
}
