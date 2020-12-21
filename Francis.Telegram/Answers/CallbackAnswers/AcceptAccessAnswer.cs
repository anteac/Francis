using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class AcceptAccessAnswer : TelegramAnswer
    {
        public override bool Public => true;

        public override bool CanProcess => Context.Command == $"/accept_access_request";


        public AcceptAccessAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            long chatId = long.Parse(Context.Parameters[0]);

            var chat = await Context.Bot.Client.GetChatAsync(chatId);
            var user = Context.Database.BotUsers.First(x => x.TelegramId == chatId);
            user.Username = chat.Username;

            await Context.Bot.SendMessage(chatId, $"Your access request has been accepted! You can now start using Francis. 😊");
            await Context.Bot.EditMessage(Context.Message, $"Access request has been accepted for user \"{chat.Username}\"!");

            Context.Logger.LogInformation($"Access request from user '{chat.Username}' has just been accepted");
        }
    }
}
