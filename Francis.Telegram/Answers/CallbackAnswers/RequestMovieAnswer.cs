using Francis.Models.Notification;
using System;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RequestMovieAnswer : CallbackAnswer
    {
        internal override bool CanProcess => Command == $"/chose_{RequestType.Movie}";


        public RequestMovieAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            var result = await Ombi.GetMovie(long.Parse(Parameters[0]));

            await HandleNewQuery(result);
        }
    }
}
