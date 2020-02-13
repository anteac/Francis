using Francis.Database.Entities;
using Francis.Models.Notification;
using System;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class CancelRequestAnswer : CallbackAnswer
    {
        internal override bool CanProcess => Command == $"/cancel";


        public CancelRequestAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            var progression = Progression as RequestProgression ?? throw new InvalidOperationException("Unknown progress status");
            progression.Status = RequestStatus.Canceled;

            if (!string.IsNullOrEmpty(Data.Message.Caption))
            {
                await Bot.Client.EditMessageCaptionAsync(chatId: Data.Message.Chat, messageId: Data.Message.MessageId, "Aye aye sir, I canceled your request !");
            }
            else
            {
                await Bot.Client.EditMessageTextAsync(chatId: Data.Message.Chat, messageId: Data.Message.MessageId, "Aye aye sir, I canceled your request !");
            }
        }
    }
}
