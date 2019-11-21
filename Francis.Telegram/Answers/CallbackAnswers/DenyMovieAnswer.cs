using Francis.Models.Notification;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class DenyMovieAnswer : CallbackAnswer
    {
        internal override bool CanProcess => Command == $"/deny_{RequestType.Movie}";


        public DenyMovieAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            long requestId = long.Parse(Parameters[0]);

            var requests = await Ombi.GetMovieRequests();
            var request = requests.First(x => x.Id == requestId);
            await Ombi.DenyMovie(new { id = requestId });

            //TODO: Find a way to give a reason
            await Bot.Client.EditMessageCaptionAsync(Data.Message.Chat, Data.Message.MessageId, Data.Message.Caption + "\n\nDenied...");

            Logger.LogInformation($"{RequestType.Movie} '{request.Title}' (requested by user '{User.UserName}') has been denied");
        }
    }
}
