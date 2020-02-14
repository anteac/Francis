using Francis.Telegram.Contexts;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers
{
    public abstract class TelegramAnswer
    {
        internal AnswerContext Context { get; set; }


        internal virtual bool Public => false;

        internal abstract bool CanProcess { get; }

        internal virtual int Priority => 0;


        public TelegramAnswer(AnswerContext context)
        {
            Context = context;
        }


        public abstract Task Execute();
    }
}
