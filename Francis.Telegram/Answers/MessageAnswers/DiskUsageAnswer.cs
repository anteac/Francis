using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class DiskUsageAnswer : MessageAnswer
    {
        internal override bool CanProcess => Command == "/disk";


        public DiskUsageAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            var sizes = new[] { "B", "KB", "MB", "GB", "TB" };
            double length = new DriveInfo(Options.Value.MediaLocation).AvailableFreeSpace;
            int order;
            for (order = 0; length >= 1024 && order < sizes.Length - 1; order++)
            {
                length /= 1024;
            }
            var result = string.Format("{0:0.##} {1}", length, sizes[order]);

            await Bot.Client.SendTextMessageAsync(Data.Chat, result);

            Logger.LogInformation($"User '{User.UserName}' requested remaining disk storage: {result}");
        }
    }
}
