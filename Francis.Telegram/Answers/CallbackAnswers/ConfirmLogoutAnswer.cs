using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
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
            Context.Database.RemoveRange(Context.Database.Progressions.Where(x => x.ChatId == Context.User.Id));
            Context.Database.RemoveRange(Context.Database.WatchedItems.Where(x => x.UserId == Context.User.Id));
            Context.Database.Remove(Context.User);

            await Context.Bot.EditMessage(Context.Message, @"
Alright, I delete everything!

...
...
...

Who are you again?
");
        }
    }
}
