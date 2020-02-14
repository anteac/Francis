using Francis.Database;
using Francis.Models;
using Francis.Models.Options;
using Francis.Services.Clients;
using Francis.Telegram.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace Francis.Telegram.Contexts
{
    public class CallbackAnswerContext : AnswerContext
    {
        public CallbackAnswerContext(
            DataCapture<CallbackQuery> capture,
            BotDbContext context,
            ITelegramClient bot,
            IOptionsSnapshot<TelegramOptions> options,
            IBotOmbiService ombi,
            ILogger<CallbackAnswerContext> logger
        ) : base(capture.Data?.Data, capture.Data?.Message, context, bot, options, ombi, logger)
        { }
    }
}
