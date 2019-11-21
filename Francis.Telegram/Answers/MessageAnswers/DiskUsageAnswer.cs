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
            double lenght = new DriveInfo(Options.Value.MediaLocation.Split('/')[0] + '/').AvailableFreeSpace;
            int order;
            for (order = 0; lenght >= 1024 && order < sizes.Length - 1; order++)
            {
                lenght /= 1024;
            }
            var result = string.Format("{0:0.##} {1}", lenght, sizes[order]);

            await Bot.Client.SendTextMessageAsync(Data.Chat, result);

            Logger.LogInformation($"User '{User.UserName}' requested remaining disk storage: {result}");
        }
    }
}
