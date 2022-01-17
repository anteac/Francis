using Francis.Database;
using Francis.Models;
using Francis.Telegram.Answers;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Extensions.Polling;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types.Enums;

namespace Francis.Telegram.Client
{
    public class TelegramUpdateHandler : IUpdateHandler
    {
        private readonly ITelegramClient _telegram;

        public TelegramUpdateHandler(ITelegramClient telegram)
        {
            _telegram = telegram;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
                await OnMessage(update.Message);

            if (update.Type == UpdateType.CallbackQuery)
                await OnCallbackQuery(update.CallbackQuery);
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            _telegram.Logger.LogError(exception, $"An global receive error occured");
            return Task.CompletedTask;
        }

        private async Task OnMessage(Message message)
        {
            try
            {
                using var scope = _telegram.Provider.CreateScope();
                scope.ServiceProvider.GetRequiredService<DataCapture<Message>>().Data = message;

                var context = scope.ServiceProvider.GetService<BotDbContext>();
                var user = context.BotUsers.FirstOrDefault(x => x.TelegramId == message.Chat.Id);

                var answer = scope.ServiceProvider.GetServices<ITelegramAnswer>()
                    .OrderByDescending(x => x.Priority)
                    .FirstOrDefault(x => x.Public == !(user?.Authorized ?? false) && x.CanProcess);

                await answer.Execute();

                answer.Context.Database.SaveChanges();
            }
            catch (Exception ex)
            {
                _telegram.Logger.LogError(ex, $"An error occured while processing user message: {message.Text}");

                await _telegram.SendMessage(message.Chat, "Something went wrong... Please verify your query. If the problem persists, contact the administrator.");
            }
        }

        private async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            await _telegram.Client.AnswerCallbackQueryAsync(callbackQuery.Id);
            await _telegram.EditMessage(callbackQuery.Message, "Please wait a bit, I'm working on it...");

            try
            {
                using var scope = _telegram.Provider.CreateScope();

                scope.ServiceProvider.GetRequiredService<DataCapture<Message>>().Data = callbackQuery.Message;
                scope.ServiceProvider.GetRequiredService<DataCapture<CallbackQuery>>().Data = callbackQuery;

                var context = scope.ServiceProvider.GetService<BotDbContext>();
                var user = context.BotUsers.FirstOrDefault(x => x.TelegramId == callbackQuery.Message.Chat.Id);

                var answer = scope.ServiceProvider.GetServices<ITelegramAnswer>()
                    .OrderByDescending(x => x.Priority)
                    .FirstOrDefault(x => x.Public == !(user?.Authorized ?? false) && x.CanProcess);

                await answer.Execute();

                answer.Context.Database.SaveChanges();
            }
            catch (Exception ex)
            {
                _telegram.Logger.LogError(ex, $"An error occured while processing user callback query: {callbackQuery.Data}");

                await _telegram.SendMessage(callbackQuery.Message.Chat, "Something went wrong... Please restart from the beginning. If the problem persists, contact the administrator.");
            }
        }
    }
}
