using Francis.Models.Notification;
using System;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RequestTvAnswer : CallbackAnswer
    {
        internal override bool CanProcess => Command == $"/chose_{RequestType.TvShow}";


        public RequestTvAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            var result = await Ombi.GetTv(long.Parse(Parameters[0]));

            await HandleNewQuery(result);
        }
    }
}
