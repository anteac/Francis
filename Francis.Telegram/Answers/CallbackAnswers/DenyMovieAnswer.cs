using Francis.Models.Notification;
using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class DenyMovieAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == $"/deny_{MediaType.Movie}";


        public DenyMovieAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            long requestId = long.Parse(Context.Parameters[0]);

            var requests = await Context.OmbiAdmin.GetMovieRequests();
            var request = requests.First(x => x.Id == requestId);
            await Context.OmbiAdmin.DenyMovie(new { id = requestId });

            await Context.Bot.EditMessage(Context.Message, Context.Message.Caption + "\n\nDenied...");

            Context.Logger.LogInformation($"{MediaType.Movie} '{request.Title}' has been denied");
        }
    }
}
