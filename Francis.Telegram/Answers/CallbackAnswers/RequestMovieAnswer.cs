using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RequestMovieAnswer : RequestMediaAnswer
    {
        internal override bool CanProcess => Context.Command == $"/chose_{RequestType.Movie}";


        public RequestMovieAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            var result = await Context.Ombi.GetMovie(long.Parse(Context.Parameters[1]));

            await HandleNewRequest(result);
        }
    }
}
