using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Francis.Telegram.Client
{
    public interface ITelegramClient
    {
        public ITelegramBotClient Client { get; }

        public bool Running { get; }


        public void Initialize();

        public Task<User> Test();
    }
}
