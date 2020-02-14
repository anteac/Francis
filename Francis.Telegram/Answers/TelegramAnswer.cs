using Francis.Database.Entities;
using Francis.Telegram.Contexts;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers
{
    public abstract class TelegramAnswer<TProgression> : ITelegramAnswer
        where TProgression : Progression
    {
        IAnswerContext ITelegramAnswer.Context => Context;

        public AnswerContext<TProgression> Context { get; set; }


        public virtual bool Public => false;

        public abstract bool CanProcess { get; }

        public virtual int Priority => 0;


        public TelegramAnswer(AnswerContext<TProgression> context)
        {
            Context = context;
        }


        public abstract Task Execute();
    }

    public abstract class TelegramAnswer : TelegramAnswer<Progression>
    {
        public TelegramAnswer(AnswerContext<Progression> context) : base(context)
        { }
    }
}
