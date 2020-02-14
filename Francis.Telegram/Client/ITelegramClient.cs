using Telegram.Bot;

namespace Francis.Telegram.Client
{
    public interface ITelegramClient
    {
        ITelegramBotClient Client { get; }

        bool Running { get; }


        void Initialize();
    }
}
