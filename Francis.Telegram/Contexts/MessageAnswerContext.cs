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
    public class MessageAnswerContext<TProgression> : AnswerContext<TProgression>
        where TProgression : Progression
    {
        public MessageAnswerContext(
            DataCapture<Message> capture,
            BotDbContext context,
            ITelegramClient bot,
            IOptionsSnapshot<TelegramOptions> options,
            IOmbiService ombiAdmin,
            IBotOmbiService ombi,
            ILogger<MessageAnswerContext<TProgression>> logger
        ) : base(capture.Data?.Text, capture?.Data, context, bot, options, ombiAdmin, ombi, logger)
        { }
    }

    public class MessageAnswerContext : MessageAnswerContext<Progression>
    {
        public MessageAnswerContext(
            DataCapture<Message> capture,
            BotDbContext context,
            ITelegramClient bot,
            IOptionsSnapshot<TelegramOptions> options,
            IOmbiService ombiAdmin,
            IBotOmbiService ombi,
            ILogger<MessageAnswerContext> logger
        ) : base(capture, context, bot, options, ombiAdmin, ombi, logger)
        { }
    }
}
