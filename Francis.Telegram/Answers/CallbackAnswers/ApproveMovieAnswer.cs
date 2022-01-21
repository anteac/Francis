using Francis.Models.Notification;
using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class ApproveMovieAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == $"/approve_{MediaType.Movie}";


        public ApproveMovieAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            long requestId = long.Parse(Context.Parameters[0]);

            var requests = await Context.OmbiAdmin.GetMovieRequests();
            var request = requests.First(x => x.Id == requestId);
            await Context.OmbiAdmin.ApproveMovie(new { id = requestId });

            await Context.Bot.EditMessage(Context.Message, Context.Message.Caption + "\n\nApproved !");

            Context.Logger.LogInformation($"{MediaType.Movie} '{request.Title}' has been approved");
        }
    }
}
