using Francis.Models.Notification;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class ApproveTvAnswer : CallbackAnswer
    {
        internal override bool CanProcess => Command == $"/approve_{RequestType.TvShow}";


        public ApproveTvAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            long requestId = long.Parse(Parameters[0]);

            var requests = await Ombi.GetTvRequests();
            var request = requests.First(x => x.Id == requestId);
            await Ombi.ApproveTv(new { id = requestId });

            await Bot.Client.EditMessageCaptionAsync(Data.Message.Chat, Data.Message.MessageId, Data.Message.Caption + "\n\nApproved !");

            Logger.LogInformation($"{RequestType.TvShow} '{request.Title}' has been approved");
        }
    }
}
