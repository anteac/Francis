using Francis.Database;
using Francis.Database.Entities;
using Francis.Models.Notification;
using Francis.Models.Options;
using Francis.Telegram.Client;
using Francis.Telegram.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Controllers
{
    [ApiController]
    [Route("webhook")]
    public class WebhookController : ControllerBase
    {
        private readonly BotDbContext _context;

        private readonly ITelegramClient _client;

        private readonly IOptionsSnapshot<TelegramOptions> _options;


        public WebhookController(BotDbContext context, ITelegramClient client, IOptionsSnapshot<TelegramOptions> options)
        {
            _context = context;
            _client = client;
            _options = options;
        }


        [HttpPost]
        public async Task Webhook(Notification notification)
        {
            if (notification.NotificationType == NotificationType.Test)
            {
                await HandleTest();
            }

            if (!long.TryParse(notification.RequestId, out var requestId))
            {
                return;
            }

            var handler = notification.NotificationType switch
            {
                NotificationType.NewRequest => HandleNewRequest(notification, requestId),
                NotificationType.RequestApproved => HandleRequestApproved(notification, requestId),
                NotificationType.RequestDeclined => HandleRequestDenied(notification, requestId),
                NotificationType.RequestAvailable => HandleRequestAvailable(notification, requestId),
                _ => Task.CompletedTask,
            };
            await handler;
        }

        private string FormatAnswer(Notification notification, string message)
        {
            //TODO: Messy seasons and episodes: maybe there's a way to change Ombi's behavior to send more accurate data?
            var result = $"{notification.Title} ({notification.Type} - {notification.Year})";
            if (notification.Type == MediaType.Tv)
            {
                result += $"\n\nSeason(s) concerned: {notification.SeasonsList}\nEpisode(s) concerned: {notification.EpisodesList}";
            }
            return $"{result}\n\n{message}";
        }

        private async Task HandleTest()
        {
            await _client.SendMessage(_options.Value.AdminChat, "This is a test message from Ombi! If you received this, your configuration is valid.");
        }

        private async Task HandleNewRequest(Notification notification, long requestId)
        {
            if (notification.IssueUser == "Francis") return;

            var formatted = FormatAnswer(notification, string.Empty);
            var message = $"The user {notification.IssueUser} has requested item: {formatted}";

            await _client.SendImage(_options.Value.AdminChat, notification.PosterImage, message, new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Approve", $"/approve_{notification.Type} {notification.RequestId}"),
                InlineKeyboardButton.WithCallbackData("Deny", $"/deny_{notification.Type} {notification.RequestId}"),
            }));
        }

        private async Task HandleRequestApproved(Notification notification, long requestId)
        {
            var users = _context.BotUsers.Where(x => x.WatchedItems.Any(x => x.RequestId == requestId && x.ItemType == notification.Type)).ToList();
            foreach (BotUser user in users)
            {
                await _client.SendMessage(user.TelegramId, FormatAnswer(notification,
                    "You're request has been approved. It will be available soon!"));
            }
        }

        private async Task HandleRequestDenied(Notification notification, long requestId)
        {
            var users = _context.BotUsers.Where(x => x.WatchedItems.Any(x => x.RequestId == requestId && x.ItemType == notification.Type)).ToList();
            foreach (BotUser user in users)
            {
                await _client.SendMessage(user.TelegramId, FormatAnswer(notification,
                    "You're request has been denied... Maybe your request doesn't match the conditions?"));
            }
        }

        private async Task HandleRequestAvailable(Notification notification, long requestId)
        {
            var users = _context.BotUsers.Where(x => x.WatchedItems.Any(x => x.RequestId == requestId && x.ItemType == notification.Type) || x.TelegramId == _options.Value.AdminChat).ToList();
            foreach (BotUser user in users)
            {
                await _client.SendMessage(user.TelegramId, FormatAnswer(notification,
                    "You're request is available. You can watch it now!"));
            }
        }
    }
}
