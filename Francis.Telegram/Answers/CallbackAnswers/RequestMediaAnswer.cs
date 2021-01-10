using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public abstract class RequestMediaAnswer : TelegramAnswer<RequestProgression>
    {
        public RequestMediaAnswer(CallbackAnswerContext<RequestProgression> context) : base(context)
        { }


        protected async Task HandleNewRequest(RequestItem item)
        {
            Context.Progression.Status = RequestStatus.Success;

            var message = item.OmbiStatus switch
            {
                RequestOmbiStatus.Requested => "has already been requested! I will tell you when it will be approved.",
                RequestOmbiStatus.Denied => "has already been requested and denied... Maybe your request doesn't match the conditions?",
                RequestOmbiStatus.Approved => "has already been requested and approved! I will tell you when it becomes available.",
                RequestOmbiStatus.Available => "is already available. You can watch it now !",
                _ => null,
            };

            if (message != null)
            {
                Context.Database.WatchedItems.Add(WatchedItem.From(item, Context.User));
                await Context.Bot.EditMessage(Context.Message, $"This {item.Type} {message}", item);
                Context.Logger.LogInformation($"User {await Context.GetName()} requested an already {item.OmbiStatus} item: {item.Title} ({item.Type} - {item.Year})");
                return;
            }

            var ombiService = Context.IsAdmin ? Context.Ombi : Context.Ombi;
            switch (item.Type)
            {
                case RequestType.Movie:
                    item.RequestId = (await ombiService.RequestMovie(new { theMovieDbId = item.Id })).RequestId;
                    break;
                case RequestType.TvShow:
                    item.RequestId = (await ombiService.RequestTv(new { tvDbId = item.Id, seasons = item.Seasons })).RequestId;
                    break;
            }

            Context.Database.WatchedItems.Add(WatchedItem.From(item, Context.User));

            await Context.Bot.EditMessage(Context.Message, $"The {item.Type} has been added to the request queue! I will tell you when it will be approved.", item);
            Context.Logger.LogInformation($"User {await Context.GetName()} has just requested item: {item.Title} ({item.Type} - {item.Year})");

            await NotifyAdministrator(item);
        }

        private async Task NotifyAdministrator(RequestItem item)
        {
            var message = $"The user {await Context.GetName()} has requested item: {item.Title} ({item.Type} - {item.Year})";
            if (item.Type == RequestType.TvShow)
            {
                var seasons = item.Seasons.Select(x => $"- Season {x.SeasonNumber} ({x.Episodes.Count} episodes)");
                message += $"\n\n{string.Join('\n', seasons)}";
            }

            await Context.Bot.SendImage(Context.Options.Value.AdminChat, item.Image, message, new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Approve", $"/approve_{item.Type} {item.RequestId}"),
                InlineKeyboardButton.WithCallbackData("Deny", $"/deny_{item.Type} {item.RequestId}"),
            }));
        }
    }
}
