using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class ApproveTvAnswer : TelegramAnswer
    {
        internal override bool CanProcess => Context.Command == $"/approve_{RequestType.TvShow}";


        public ApproveTvAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            long requestId = long.Parse(Context.Parameters[0]);

            var requests = await Context.Ombi.GetTvRequests();
            var request = requests.First(x => x.Id == requestId);
            await Context.Ombi.ApproveTv(new { id = requestId });

            await Context.Bot.Client.EditMessageCaptionAsync(Context.Message.Chat, Context.Message.MessageId, Context.Message.Caption + "\n\nApproved !");

            Context.Logger.LogInformation($"{RequestType.TvShow} '{request.Title}' has been approved");
        }
    }
}
