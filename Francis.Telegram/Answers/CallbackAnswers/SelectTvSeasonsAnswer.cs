using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class SelectTvSeasonsAnswer : TelegramAnswer
    {
        public override bool CanProcess => Context.Command == $"/chose_{RequestType.TvShow}";


        public SelectTvSeasonsAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            var result = await Context.Ombi.GetTv(long.Parse(Context.Parameters[1]));

            await Context.Bot.EditMessage(Context.Message, $"I'm about to send the request. Can you please tell me which season(s) you want?", result, new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData($"{TvShowSeasons.First}", $"/seasons {Context.Progression.Id} {result.TheTvDbId} {TvShowSeasons.First}"),
                    InlineKeyboardButton.WithCallbackData($"{TvShowSeasons.Last}", $"/seasons {Context.Progression.Id} {result.TheTvDbId} {TvShowSeasons.Last}"),
                    InlineKeyboardButton.WithCallbackData($"{TvShowSeasons.All}", $"/seasons {Context.Progression.Id} {result.TheTvDbId} {TvShowSeasons.All}"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Cancel request", $"/cancel {Context.Progression.Id}"),
                }
            }));

            Context.Logger.LogInformation($"User '{Context.User.UserName}' is requesting {RequestType.TvShow} '{result.Title}'. Waiting for seasons selection.");
        }
    }
}
