using Francis.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Francis.Telegram.Client
{
    public class TelegramClient : ITelegramClient
    {
        private CancellationTokenSource _token;

        public IServiceProvider Provider { get; private set; }

        public IOptionsMonitor<TelegramOptions> Options { get; private set; }

        public ILogger<ITelegramClient> Logger { get; private set; }

        public ITelegramBotClient Client { get; private set; }


        public TelegramClient(IServiceProvider provider, IOptionsMonitor<TelegramOptions> options, ILogger<TelegramClient> logger)
        {
            Provider = provider;
            Options = options;
            Logger = logger;
        }

        public async Task<bool> Initialize()
        {
            if (_token != null)
                _token.Cancel();

            _token = new CancellationTokenSource();

            var handler = new TelegramUpdateHandler(this);

            if (string.IsNullOrEmpty(Options.CurrentValue.BotToken))
                return false;

            Client = new TelegramBotClient(Options.CurrentValue.BotToken);
            Client.StartReceiving(handler, cancellationToken: _token.Token);

            return await Client.TestApiAsync();
        }

        public async Task<bool> IsRunning() => Client != null && await Client.TestApiAsync();
    }
}
