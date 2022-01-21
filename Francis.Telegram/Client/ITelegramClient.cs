using System;
using System.Threading.Tasks;
using Francis.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Francis.Telegram.Client
{
    public interface ITelegramClient
    {
        IServiceProvider Provider { get; }

        IOptionsMonitor<TelegramOptions> Options { get; }

        ILogger<ITelegramClient> Logger { get; }

        ITelegramBotClient Client { get; }


        Task<bool> Initialize();

        Task<bool> IsRunning();
    }
}
