using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class ConfirmLogoutAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == $"/confirm_logout";


        public ConfirmLogoutAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            Context.Database.RemoveRange(Context.Database.Progressions.Where(x => x.BotUser.TelegramId == Context.User.TelegramId));
            Context.Database.RemoveRange(Context.Database.WatchedItems.Where(x => x.BotUser.TelegramId == Context.User.TelegramId));
            Context.Database.Remove(Context.User);

            if (Context.Message.Chat.Id != Context.Options.Value.AdminChat)
            {
                await Context.Bot.SendMessage(Context.Options.Value.AdminChat, $"{await Context.GetName()} has just successfully logout.");
            }
            await Context.Bot.EditMessage(Context.Message, @"
Alright, I delete everything!

...
...
...

Who are you again?
");

            Context.Logger.LogInformation($"Telegram user '{Context.Message.Chat.Username}' ({Context.Message.Chat.FirstName} {Context.Message.Chat.LastName}) successfully logged out");
        }
    }
}
