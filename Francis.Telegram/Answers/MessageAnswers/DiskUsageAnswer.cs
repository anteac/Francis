using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class DiskUsageAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == "/disk";


        public DiskUsageAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            var sizes = new[] { "B", "KB", "MB", "GB", "TB" };
            double length = new DriveInfo(Context.Options.Value.MediaLocation).AvailableFreeSpace;
            int order;
            for (order = 0; length >= 1024 && order < sizes.Length - 1; order++)
            {
                length /= 1024;
            }
            var result = string.Format("{0:0.##} {1}", length, sizes[order]);

            await Context.Bot.SendMessage(Context.Message.Chat, result);

            Context.Logger.LogInformation($"User {await Context.GetName()} requested remaining disk storage: {result}");
        }
    }
}
