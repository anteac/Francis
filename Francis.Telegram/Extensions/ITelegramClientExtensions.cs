using Francis.Models;
using Francis.Telegram.Client;
using Francis.Toolbox.Extensions;
using Francis.Toolbox.Helpers;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Extensions
{
    public static class ITelegramClientExtensions
    {
        public static async Task<Message> SendMessage(this ITelegramClient bot, ChatId chatId, string text = null, InlineKeyboardMarkup replies = null)
        {
            return await bot.Client.SendTextMessageAsync(
                chatId: chatId,
                text: text,
                replyMarkup: replies
            );
        }

        public static async Task<Message> SendImage(this ITelegramClient bot, ChatId chatId, string imageUrl, string text = null, InlineKeyboardMarkup replies = null)
        {
            var defaultPoster = Assembly.GetExecutingAssembly().GetResource("Assets.default_poster.png");
            if (imageUrl != null)
            {
                imageUrl = imageUrl.StartsWith("https://") ? imageUrl : Path.Combine("https://image.tmdb.org/t/p/w300/", imageUrl.Trim('/'));
            }
            var image = await WebResource.Exists(imageUrl) ? new InputMedia(imageUrl) : new InputMedia(defaultPoster, "default_poster");

            return await bot.Client.SendPhotoAsync(
                chatId: chatId,
                photo: image,
                caption: text,
                replyMarkup: replies
            );
        }

        public static async Task<Message> EditMessage(this ITelegramClient bot, Message message, string text = null, RequestItem item = null, InlineKeyboardMarkup replies = null)
        {
            if (message.Type == MessageType.Photo)
            {
                return await bot.Client.EditMessageCaptionAsync(
                    chatId: message.Chat,
                    messageId: message.MessageId,
                    caption: item?.AsString(text) ?? text ?? message.Caption,
                    replyMarkup: replies
                );
            }

            return await bot.Client.EditMessageTextAsync(
                chatId: message.Chat,
                messageId: message.MessageId,
                text: item?.AsString(text) ?? text ?? message.Text,
                replyMarkup: replies
            );
        }

        public static async Task<Message> EditImage(this ITelegramClient bot, Message message, string imageUrl, string text = null, RequestItem item = null, InlineKeyboardMarkup replies = null)
        {
            var defaultPoster = Assembly.GetExecutingAssembly().GetResource("Assets.default_poster.png");
            if (imageUrl != null)
            {
                imageUrl = imageUrl.StartsWith("https://") ? imageUrl : Path.Combine("https://image.tmdb.org/t/p/w300/", imageUrl.Trim('/'));
            }
            var image = await WebResource.Exists(imageUrl) ? new InputMedia(imageUrl) : new InputMedia(defaultPoster, "default_poster");

            if (message.Type == MessageType.Photo && message.From.IsBot)
            {
                return await bot.Client.EditMessageMediaAsync(
                    chatId: message.Chat,
                    messageId: message.MessageId,
                    media: new InputMediaPhoto(image) { Caption = item?.AsString(text) ?? text ?? message.Caption },
                    replyMarkup: replies
                );
            }

            if (message.From.IsBot)
            {
                await bot.Client.DeleteMessageAsync(message.Chat, message.MessageId);
            }

            return await bot.Client.SendPhotoAsync(
                chatId: message.Chat,
                photo: image,
                caption: item?.AsString(text) ?? text ?? message.Text,
                replyMarkup: replies
            );
        }

        public static async Task<string> GetName(this ITelegramClient bot, long telegramId)
        {
            var chat = await bot.Client.GetChatAsync(telegramId);
            var username = string.Empty;
            if (!string.IsNullOrWhiteSpace(chat.Username))
            {
                username += $"'{chat.Username}'";
            }
            if (!string.IsNullOrWhiteSpace(chat.FirstName) || !string.IsNullOrWhiteSpace(chat.LastName))
            {
                var fullname = $"{chat.FirstName} {chat.LastName}".Trim();
                username = !string.IsNullOrWhiteSpace(username) ? $"{username} ({fullname})" : fullname;
            }
            return username;
        }
    }
}
