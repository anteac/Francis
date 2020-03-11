using Francis.Database;
using Francis.Database.Entities;
using Francis.Models.Options;
using Francis.Services.Clients;
using Francis.Telegram.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;

namespace Francis.Telegram.Contexts
{
    public interface IAnswerContext
    {
        BotDbContext Database { get; }
    }

    public class AnswerContext<TProgression> : IAnswerContext
        where TProgression : Progression
    {
        public Message Message { get; set; }


        public BotDbContext Database { get; }

        public ITelegramClient Bot { get; }

        public IOptionsSnapshot<TelegramOptions> Options { get; }

        public IBotOmbiService Ombi { get; }

        public ILogger Logger { get; }


        public BotUser User { get; set; }

        public string Command { get; set; }

        public string[] Parameters { get; set; }


        public TProgression Progression => Database.Progressions.FirstOrDefault(x => x.Id == int.Parse(Parameters[0])) as TProgression;


        public AnswerContext(
            string fullCommand,
            Message message,
            BotDbContext context,
            ITelegramClient bot,
            IOptionsSnapshot<TelegramOptions> options,
            IBotOmbiService ombi,
            ILogger<AnswerContext<TProgression>> logger
        )
        {
            Message = message;
            Database = context;
            Bot = bot;
            Options = options;
            Ombi = ombi;
            Logger = logger;

            if (Message != null)
            {
                User = Database.BotUsers.FirstOrDefault(x => x.TelegramId == Message.Chat.Id);
            }

            if (fullCommand != null)
            {
                var parameters = fullCommand.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var queue = new Queue<string>(parameters);

                Command = queue.Dequeue();
                Parameters = queue.ToArray();
            }
        }
    }
}
