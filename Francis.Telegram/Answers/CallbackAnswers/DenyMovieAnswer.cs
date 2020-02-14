using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class DenyMovieAnswer : TelegramAnswer
    {
        internal override bool CanProcess => Context.Command == $"/deny_{RequestType.Movie}";


        public DenyMovieAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            //TODO: Add ability to give a deny reasons

            long requestId = long.Parse(Context.Parameters[0]);

            var requests = await Context.Ombi.GetMovieRequests();
            var request = requests.First(x => x.Id == requestId);
            await Context.Ombi.DenyMovie(new { id = requestId });

            //TODO: Find a way to give a reason
            await Context.Bot.Client.EditMessageCaptionAsync(Context.Message.Chat, Context.Message.MessageId, Context.Message.Caption + "\n\nDenied...");

            Context.Logger.LogInformation($"{RequestType.Movie} '{request.Title}' has been denied");
        }
    }
}
