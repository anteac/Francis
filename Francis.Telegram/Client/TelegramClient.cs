using Francis.Database;
using Francis.Extensions;
using Francis.Models;
using Francis.Models.Options;
using Francis.Telegram.Answers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Francis.Telegram.Client
{
    public class TelegramClient : ITelegramClient
    {
        private readonly IServiceProvider _provider;
        private readonly IOptionsMonitor<TelegramOptions> _options;
        private readonly ILogger<TelegramClient> _logger;

        public ITelegramBotClient Client { get; private set; }

        public bool Running => Client != null && Client.IsReceiving;


        public TelegramClient(IServiceProvider provider, IOptionsMonitor<TelegramOptions> options, ILogger<TelegramClient> logger)
        {
            _provider = provider;
            _options = options;
            _logger = logger;
        }


        public void Initialize()
        {
            if (Client != null && Client.IsReceiving)
            {
                Client.StopReceiving();
            }

            Client = new TelegramBotClient(_options.CurrentValue.BotToken);
            Client.OnMessage += OnMessage;
            Client.OnCallbackQuery += OnCallbackQuery;

            Client.StartReceiving();
        }

        public async Task<User> Test()
        {
            Initialize();

            return await Client.GetMeAsync();
        }


        private async void OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                using var scope = _provider.CreateScope();
                scope.ServiceProvider.GetRequiredService<DataCapture<Message>>().Data = e.Message;

                var context = scope.ServiceProvider.GetService<BotDbContext>();
                var user = context.BotUsers.Find(e.Message.Chat.Id);

                var answer = scope.ServiceProvider.GetServices<TelegramAnswer>()
                    .OrderByDescending(x => x.Priority)
                    .FirstOrDefault(x => x.Public == !(user?.PlexToken != null) && x.CanProcess);

                await answer.Execute();

                answer.Context.Database.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while processing user message: {e.Message.Text}");

                await Client.SendTextMessageAsync(e.Message.Chat, "Something went wrong... Please verify your query. If the problem persists, contact the administrator.");
            }
        }

        private async void OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            await Client.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
            if (!string.IsNullOrWhiteSpace(e.CallbackQuery.Message.Text))
            {
                await this.EditText(e.CallbackQuery.Message);
            }
            if (!string.IsNullOrWhiteSpace(e.CallbackQuery.Message.Caption))
            {
                await this.EditCaption(e.CallbackQuery.Message);
            }

            try
            {
                using var scope = _provider.CreateScope();

                scope.ServiceProvider.GetRequiredService<DataCapture<Message>>().Data = e.CallbackQuery.Message;
                scope.ServiceProvider.GetRequiredService<DataCapture<CallbackQuery>>().Data = e.CallbackQuery;

                var context = scope.ServiceProvider.GetService<BotDbContext>();
                var user = context.BotUsers.Find(e.CallbackQuery.Message.Chat.Id);

                var answer = scope.ServiceProvider.GetServices<TelegramAnswer>()
                    .OrderByDescending(x => x.Priority)
                    .FirstOrDefault(x => x.Public == !(user?.PlexToken != null) && x.CanProcess);

                await answer.Execute();

                answer.Context.Database.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while processing user callback query: {e.CallbackQuery.Data}");

                await Client.SendTextMessageAsync(e.CallbackQuery.Message.Chat, "Something went wrong... Please restart from the beginning. If the problem persists, contact the administrator.");
            }
        }
    }
}
