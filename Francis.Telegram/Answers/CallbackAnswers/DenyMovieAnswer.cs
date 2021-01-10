using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class DenyMovieAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == $"/deny_{RequestType.Movie}";


        public DenyMovieAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            long requestId = long.Parse(Context.Parameters[0]);

            var requests = await Context.OmbiAdmin.GetMovieRequests();
            var request = requests.First(x => x.Id == requestId);
            await Context.OmbiAdmin.DenyMovie(new { id = requestId });

            await Context.Bot.EditMessage(Context.Message, Context.Message.Caption + "\n\nDenied...");

            Context.Logger.LogInformation($"{RequestType.Movie} '{request.Title}' has been denied");
        }
    }
}
