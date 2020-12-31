using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RejectAccessAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == $"/reject_access_request";


        public RejectAccessAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            long chatId = long.Parse(Context.Parameters[0]);

            await Context.Bot.SendMessage(chatId, $"Your access request has been rejected...");
            await Context.Bot.EditMessage(Context.Message, $"Access request has been rejected for user {await Context.Bot.GetName(chatId)}.");

            Context.Logger.LogInformation($"Access request from user {await Context.Bot.GetName(chatId)} has just been rejected");
        }
    }
}
