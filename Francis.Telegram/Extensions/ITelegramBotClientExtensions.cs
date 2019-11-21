using Francis.Models;
using Francis.Telegram.Client;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Francis.Extensions
{
    public static class ITelegramBotClientExtensions
    {
        public static Task<Message> EditText(this ITelegramClient bot, Message message, string text = null, RequestItem item = null)
        {
            return bot.Client.EditMessageTextAsync(message.Chat, message.MessageId, item?.AsString(text) ?? text ?? message.Text);
        }

        public static Task<Message> EditCaption(this ITelegramClient bot, Message message, string text = null, RequestItem item = null)
        {
            return bot.Client.EditMessageCaptionAsync(message.Chat, message.MessageId, item?.AsString(text) ?? text ?? message.Caption);
        }

        public static Task<Message> RemoveActions(this ITelegramClient bot, long chatId, int messageId)
        {
            return bot.Client.EditMessageReplyMarkupAsync(chatId, messageId);
        }
    }
}
