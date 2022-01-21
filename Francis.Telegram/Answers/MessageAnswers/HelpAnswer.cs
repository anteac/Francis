using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class HelpAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == "/help" || Context.Command == "/start";


        public HelpAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            await Context.Bot.SendMessage(Context.Message.Chat, @"
Francis, at your service!

/help - Display this message
/disk - Get remaining free space
/logout - Remove all I know about you

Any other message will be considered as a search!
");

            Context.Logger.LogInformation($"User {await Context.GetName()} requested help");
        }
    }
}
