using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Notification;
using Francis.Telegram.Answers;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RequestMovieAnswer : RequestMediaAnswer
    {
        public override bool CanProcess => Context.Command == $"/chose_{MediaType.Movie}";


        public RequestMovieAnswer(AnswerContext<RequestProgression> context) : base(context)
        { }


        public override async Task Execute()
        {
            var result = await Context.Ombi.GetMovie(Context.Parameters[1]);

            await HandleNewRequest((RequestItem)result);
        }
    }
}
