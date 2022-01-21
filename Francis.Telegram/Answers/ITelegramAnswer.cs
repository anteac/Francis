using Francis.Telegram.Answers;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers
{
    public interface ITelegramAnswer
    {
        IAnswerContext Context { get; }


        bool Public { get; }

        bool CanProcess { get; }

        int Priority { get; }


        Task Execute();
    }
}
