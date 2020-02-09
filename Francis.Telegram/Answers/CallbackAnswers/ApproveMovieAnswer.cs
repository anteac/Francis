using Francis.Models.Notification;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class ApproveMovieAnswer : CallbackAnswer
    {
        internal override bool CanProcess => Command == $"/approve_{RequestType.Movie}";


        public ApproveMovieAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            long requestId = long.Parse(Parameters[0]);

            var requests = await Ombi.GetMovieRequests();
            var request = requests.First(x => x.Id == requestId);
            await Ombi.ApproveMovie(new { id = requestId });

            await Bot.Client.EditMessageCaptionAsync(Data.Message.Chat, Data.Message.MessageId, Data.Message.Caption + "\n\nApproved !");

            Logger.LogInformation($"{RequestType.Movie} '{request.Title}' has been approved");
        }
    }
}
