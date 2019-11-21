using Francis.Database.Entities;
using Francis.Extensions;
using Francis.Models;
using Francis.Models.Notification;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Francis.Telegram.Answers
{
    public abstract class CallbackAnswer : Answer<CallbackQuery>
    {
        protected override long UserId => Data.Message.Chat.Id;

        protected override string FullCommand => Data.Data;


        public CallbackAnswer(IServiceProvider provider) : base(provider)
        { }


        protected async Task HandleNewQuery(RequestItem item)
        {
            //TODO: Fix duplicate code
            if (item.Available)
            {
                Context.WatchedItems.Add(WatchedItem.From(item, User));
                await Bot.EditCaption(Data.Message, $"This {item.Type} is already available. You can watch it now !", item);
                Logger.LogInformation($"User '{User.UserName}' requested an already available item: {item.Title} ({item.Type} - {item.Year})");
                return;
            }

            if (item.Approved)
            {
                Context.WatchedItems.Add(WatchedItem.From(item, User));
                await Bot.EditCaption(Data.Message, $"This {item.Type} as already been requested and approved! I will tell you when it becomes available.", item);
                Logger.LogInformation($"User '{User.UserName}' requested an already approved item: {item.Title} ({item.Type} - {item.Year})");
                return;
            }

            if (item.Denied)
            {
                Context.WatchedItems.Add(WatchedItem.From(item, User));
                await Bot.EditCaption(Data.Message, $"This {item.Type} as already been requested and denied... Maybe your request doesn't match the conditions?", item);
                Logger.LogInformation($"User '{User.UserName}' requested an already denied item: {item.Title} ({item.Type} - {item.Year})");
                return;
            }

            if (item.Requested)
            {
                Context.WatchedItems.Add(WatchedItem.From(item, User));
                await Bot.EditCaption(Data.Message, $"This {item.Type} as already been requested! I will tell you when it will be approved.", item);
                Logger.LogInformation($"User '{User.UserName}' requested an already requested item: {item.Title} ({item.Type} - {item.Year})");
                return;
            }

            switch (item.Type)
            {
                case RequestType.Movie:
                    item.RequestId = (await Ombi.RequestMovie(new { theMovieDbId = item.Id })).RequestId;
                    break;
                case RequestType.TvShow:
                    item.RequestId = (await Ombi.RequestTv(new { tvDbId = item.Id })).RequestId;
                    break;
            }

            Context.WatchedItems.Add(WatchedItem.From(item, User));

            await Bot.EditCaption(Data.Message, $"The {item.Type} has been added to the request queue! I will tell you when it will be approved.", item);
            Logger.LogInformation($"User '{User.UserName}' has just requested item: {item.Title} ({item.Type} - {item.Year})");
        }

    }
}
