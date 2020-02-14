using Telegram.Bot;

namespace Francis.Telegram.Client
{
    public interface ITelegramClient
    {
        public ITelegramBotClient Client { get; }

        public bool Running { get; }


        public void Initialize();
    }
}
