using Francis.Database;
using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Options;
using Francis.Services.Clients;
using Francis.Telegram.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace Francis.Telegram.Contexts
{
    public class CallbackAnswerContext<TProgression> : AnswerContext<TProgression>
        where TProgression : Progression
    {
        public CallbackAnswerContext(
            DataCapture<CallbackQuery> capture,
            BotDbContext context,
            ITelegramClient bot,
            IOptionsSnapshot<TelegramOptions> options,
            IOmbiService ombiAdmin,
            IBotOmbiService ombi,
            ILogger<CallbackAnswerContext<TProgression>> logger
        ) : base(capture.Data?.Data, capture.Data?.Message, context, bot, options, ombiAdmin, ombi, logger)
        { }
    }

    public class CallbackAnswerContext : CallbackAnswerContext<Progression>
    {
        public CallbackAnswerContext(
            DataCapture<CallbackQuery> capture,
            BotDbContext context,
            ITelegramClient bot,
            IOptionsSnapshot<TelegramOptions> options,
            IOmbiService ombiAdmin,
            IBotOmbiService ombi,
            ILogger<CallbackAnswerContext> logger
        ) : base(capture, context, bot, options, ombiAdmin, ombi, logger)
        { }
    }
}
