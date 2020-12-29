using Francis.Database.Entities;
using Francis.Telegram.Client;
using Francis.Telegram.Extensions;
using System.Threading.Tasks;

namespace Francis.Models
{
    public class EnhancedBotUser : BotUser
    {
        public string Username { get; set; }

        public static async Task<EnhancedBotUser> Create(BotUser user, ITelegramClient bot) => new EnhancedBotUser
        {
            Id = user.Id,
            Authorized = user.Authorized,
            TelegramId = user.TelegramId,
            Progressions = user.Progressions,
            WatchedItems = user.WatchedItems,

            Username = await bot.GetName(user.TelegramId),
        };
    }
}
