using Telegram.Bot.Types;

namespace Francis.Models.Telegram
{
    public class AboutTelegramBot
    {
        public long Id { get; set; }
        public bool IsBot { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string LanguageCode { get; set; }


        public static implicit operator AboutTelegramBot(User telegramUser) => new AboutTelegramBot
        {
            Id = telegramUser.Id,
            IsBot = telegramUser.IsBot,
            FirstName = telegramUser.FirstName,
            LastName = telegramUser.LastName,
            Username = telegramUser.Username,
            LanguageCode = telegramUser.LanguageCode
        };
    }
}
