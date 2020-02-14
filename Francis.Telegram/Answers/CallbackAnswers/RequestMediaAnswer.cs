using Francis.Database.Entities;
using Francis.Extensions;
using Francis.Models;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public abstract class RequestMediaAnswer : TelegramAnswer
    {
        public RequestMediaAnswer(CallbackAnswerContext context) : base(context)
        { }


        protected async Task HandleNewRequest(RequestItem item)
        {
            //TODO: This method does not belong to this class
            //TODO: Fix duplicate code

            var progression = Context.Progression as RequestProgression ?? throw new InvalidOperationException("Unknown progress status");
            progression.Status = RequestStatus.Success;

            if (item.Available)
            {
                Context.Database.WatchedItems.Add(WatchedItem.From(item, Context.User));
                await Context.Bot.EditCaption(Context.Message, $"This {item.Type} is already available. You can watch it now !", item);
                Context.Logger.LogInformation($"User '{Context.User.UserName}' requested an already available item: {item.Title} ({item.Type} - {item.Year})");
                return;
            }

            if (item.Approved)
            {
                Context.Database.WatchedItems.Add(WatchedItem.From(item, Context.User));
                await Context.Bot.EditCaption(Context.Message, $"This {item.Type} as already been requested and approved! I will tell you when it becomes available.", item);
                Context.Logger.LogInformation($"User '{Context.User.UserName}' requested an already approved item: {item.Title} ({item.Type} - {item.Year})");
                return;
            }

            if (item.Denied)
            {
                Context.Database.WatchedItems.Add(WatchedItem.From(item, Context.User));
                await Context.Bot.EditCaption(Context.Message, $"This {item.Type} as already been requested and denied... Maybe your request doesn't match the conditions?", item);
                Context.Logger.LogInformation($"User '{Context.User.UserName}' requested an already denied item: {item.Title} ({item.Type} - {item.Year})");
                return;
            }

            if (item.Requested)
            {
                Context.Database.WatchedItems.Add(WatchedItem.From(item, Context.User));
                await Context.Bot.EditCaption(Context.Message, $"This {item.Type} as already been requested! I will tell you when it will be approved.", item);
                Context.Logger.LogInformation($"User '{Context.User.UserName}' requested an already requested item: {item.Title} ({item.Type} - {item.Year})");
                return;
            }

            switch (item.Type)
            {
                case RequestType.Movie:
                    item.RequestId = (await Context.Ombi.RequestMovie(new { theMovieDbId = item.Id })).RequestId;
                    break;
                case RequestType.TvShow:
                    item.RequestId = (await Context.Ombi.RequestTv(new { tvDbId = item.Id, seasons = item.Seasons })).RequestId;
                    break;
            }

            Context.Database.WatchedItems.Add(WatchedItem.From(item, Context.User));

            await Context.Bot.EditCaption(Context.Message, $"The {item.Type} has been added to the request queue! I will tell you when it will be approved.", item);
            Context.Logger.LogInformation($"User '{Context.User.UserName}' has just requested item: {item.Title} ({item.Type} - {item.Year})");
        }
    }
}
