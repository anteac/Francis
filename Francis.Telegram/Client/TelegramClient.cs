using Francis.Database;
using Francis.Models;
using Francis.Models.Options;
using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
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

        private async void OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                using var scope = _provider.CreateScope();
                scope.ServiceProvider.GetRequiredService<DataCapture<Message>>().Data = e.Message;

                var context = scope.ServiceProvider.GetService<BotDbContext>();
                var user = context.BotUsers.FirstOrDefault(x => x.TelegramId == e.Message.Chat.Id);

                var answer = scope.ServiceProvider.GetServices<ITelegramAnswer>()
                    .OrderByDescending(x => x.Priority)
                    .FirstOrDefault(x => x.Public == !(user?.Username != null) && x.CanProcess);

                await answer.Execute();

                answer.Context.Database.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while processing user message: {e.Message.Text}");

                await this.SendMessage(e.Message.Chat, "Something went wrong... Please verify your query. If the problem persists, contact the administrator.");
            }
        }

        private async void OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            await Client.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
            await this.EditMessage(e.CallbackQuery.Message, "Please wait a bit, I'm working on it...");

            try
            {
                using var scope = _provider.CreateScope();

                scope.ServiceProvider.GetRequiredService<DataCapture<Message>>().Data = e.CallbackQuery.Message;
                scope.ServiceProvider.GetRequiredService<DataCapture<CallbackQuery>>().Data = e.CallbackQuery;

                var context = scope.ServiceProvider.GetService<BotDbContext>();
                var user = context.BotUsers.FirstOrDefault(x => x.TelegramId == e.CallbackQuery.Message.Chat.Id);

                var answer = scope.ServiceProvider.GetServices<ITelegramAnswer>()
                    .OrderByDescending(x => x.Priority)
                    .FirstOrDefault(x => x.Public == !(user?.Username != null) && x.CanProcess);

                await answer.Execute();

                answer.Context.Database.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while processing user callback query: {e.CallbackQuery.Data}");

                await this.SendMessage(e.CallbackQuery.Message.Chat, "Something went wrong... Please restart from the beginning. If the problem persists, contact the administrator.");
            }
        }
    }
}
