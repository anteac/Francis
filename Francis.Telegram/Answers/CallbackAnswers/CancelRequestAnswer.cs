using Francis.Database.Entities;
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

            if (!string.IsNullOrEmpty(Context.Message.Caption))
            {
                await Context.Bot.Client.EditMessageCaptionAsync(chatId: Context.Message.Chat, messageId: Context.Message.MessageId, "Aye aye sir, I canceled your request !");
            }
            else
            {
                await Context.Bot.Client.EditMessageTextAsync(chatId: Context.Message.Chat, messageId: Context.Message.MessageId, "Aye aye sir, I canceled your request !");
            }
        }
    }
}
