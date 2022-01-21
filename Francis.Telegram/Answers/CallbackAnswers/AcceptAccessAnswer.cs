using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class AcceptAccessAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == $"/accept_access_request";


        public AcceptAccessAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            long chatId = long.Parse(Context.Parameters[0]);

            var user = Context.Database.BotUsers.First(x => x.TelegramId == chatId);
            user.Authorized = true;

            await Context.Bot.SendMessage(chatId, $"Your access request has been accepted! You can now start using Francis. 😊");
            await Context.Bot.EditMessage(Context.Message, $"Access request has been accepted for user {await Context.Bot.GetName(chatId)}!");

            Context.Logger.LogInformation($"Access request from user {await Context.Bot.GetName(chatId)} has just been accepted");
        }
    }
}
