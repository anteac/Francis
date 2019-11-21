using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class HelpAnswer : MessageAnswer
    {
        internal override bool CanProcess => Command == "/help" || Command == "/start";


        public HelpAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            await Bot.Client.SendTextMessageAsync(Data.Chat,
      @"
Francis, at your service!

/help - Display this message
/disk - Get remaining free space

Any other message will be considered as a search!
");

            Logger.LogInformation($"User '{User.UserName}' requested help");
        }
    }
}
