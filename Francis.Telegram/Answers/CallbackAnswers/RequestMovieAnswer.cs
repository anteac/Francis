using Francis.Database.Entities;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RequestMovieAnswer : RequestMediaAnswer
    {
        public override bool CanProcess => Context.Command == $"/chose_{RequestType.Movie}";


        public RequestMovieAnswer(CallbackAnswerContext<RequestProgression> context) : base(context)
        { }


        public override async Task Execute()
        {
            var result = await Context.Ombi.GetMovie(long.Parse(Context.Parameters[1]));

            await HandleNewRequest(result);
        }
    }
}
