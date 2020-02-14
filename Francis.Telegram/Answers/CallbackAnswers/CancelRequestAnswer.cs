using Francis.Database.Entities;
using Francis.Telegram.Extensions;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using System;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class CancelRequestAnswer : TelegramAnswer
    {
        internal override bool CanProcess => Context.Command == $"/cancel";


        public CancelRequestAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            var progression = Context.Progression as RequestProgression ?? throw new InvalidOperationException("Unknown progress status");
            progression.Status = RequestStatus.Canceled;

            await Context.Bot.EditMessage(Context.Message, "Aye aye sir, I canceled your request !");
        }
    }
}
