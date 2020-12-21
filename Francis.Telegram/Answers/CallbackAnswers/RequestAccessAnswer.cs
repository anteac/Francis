using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RequestAccessAnswer : TelegramAnswer
    {
        public override bool Public => true;

        public override bool CanProcess => Context.Command == $"/request_access";


        public RequestAccessAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            var userDescription = $"User \"{Context.Message.Chat.Username}\" ({Context.Message.Chat.FirstName} {Context.Message.Chat.LastName})";
            await Context.Bot.SendMessage(Context.Options.Value.AdminChat, $"{userDescription} has requested access to Francis, do you accept?", new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Accept", $"/accept_access_request {Context.Message.Chat.Id}"),
                InlineKeyboardButton.WithCallbackData("Reject", $"/reject_access_request {Context.Message.Chat.Id}")
            }));

            await Context.Bot.EditMessage(Context.Message, "Request has been sent! Please wait for the approval of the administrator.");

            Context.Logger.LogInformation($"User '{Context.Message.Chat.Username}' has just requested access to Francis");
        }
    }
}
