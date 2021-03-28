using Francis.Models.Notification;
using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class DenyTvAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == $"/deny_{MediaType.Tv}";


        public DenyTvAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            long requestId = long.Parse(Context.Parameters[0]);

            var requests = await Context.OmbiAdmin.GetTvRequests();
            var request = requests.First(x => x.Id == requestId || x.ChildRequests.Any(x => x.Id == requestId));
            await Context.OmbiAdmin.DenyTv(new { id = requestId });

            await Context.Bot.EditMessage(Context.Message, Context.Message.Caption + "\n\nDenied...");

            Context.Logger.LogInformation($"{MediaType.Tv} '{request.Title}' has been denied");
        }
    }
}
