using Francis.Database.Entities;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class CancelRequestAnswer : TelegramAnswer<RequestProgression>
    {
        public override bool CanProcess => Context.Command == $"/cancel";


        public CancelRequestAnswer(CallbackAnswerContext<RequestProgression> context) : base(context)
        { }


        public override async Task Execute()
        {
            Context.Progression.Status = RequestStatus.Canceled;

            await Context.Bot.EditMessage(Context.Message, "Aye aye sir, I canceled your request !");
        }
    }
}
