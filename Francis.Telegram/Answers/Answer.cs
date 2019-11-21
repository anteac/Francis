using Francis.Database;
using Francis.Database.Entities;
using Francis.Models;
using Francis.Options;
using Francis.Services.Clients;
using Francis.Telegram.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers
{
    public abstract class Answer<TData>
    {
        internal BotDbContext Context { get; }

        protected TData Data { get; }

        protected ITelegramClient Bot { get; }

        protected IOptionsSnapshot<TelegramOptions> Options { get; }

        protected IOmbiService Ombi { get; }

        protected ILogger Logger { get; }


        protected BotUser User { get; set; }

        protected string Command { get; private set; }

        protected string[] Parameters { get; private set; }

        protected Progression Progression => Context.Progressions.Find(int.Parse(Parameters[0]));


        internal virtual bool Public => false;

        internal abstract bool CanProcess { get; }

        protected virtual bool HasProgression => false;

        protected abstract long UserId { get; }

        protected abstract string FullCommand { get; }


        public Answer(IServiceProvider provider)
        {
            Data = provider.GetRequiredService<DataCapture<TData>>().Data;
            Context = provider.GetRequiredService<BotDbContext>();
            Bot = provider.GetRequiredService<ITelegramClient>();
            Options = provider.GetRequiredService<IOptionsSnapshot<TelegramOptions>>();
            Ombi = provider.GetRequiredService<IOmbiService>();
            Logger = provider.GetRequiredService<ILogger<Answer<TData>>>();

            User = Context.Users.Find(UserId);

            var parameters = FullCommand.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var queue = new Queue<string>(parameters);

            Command = queue.Dequeue();
            Parameters = queue.ToArray();
        }


        public abstract Task Execute();
    }
}
