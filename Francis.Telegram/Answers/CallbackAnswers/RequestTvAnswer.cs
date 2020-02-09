using Francis.Extensions;
using Francis.Models.Notification;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RequestTvAnswer : CallbackAnswer
    {
        internal override bool CanProcess => Command == $"/chose_{RequestType.TvShow}";


        public RequestTvAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            var result = await Ombi.GetTv(long.Parse(Parameters[0]));

            await Bot.EditCaption(Data.Message, $"I'm about to send the request. Can you please tell me which season(s) you want?", result, new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData($"{TvShowSeasons.First}", $"/seasons {result.TheTvDbId} {TvShowSeasons.First}"),
                InlineKeyboardButton.WithCallbackData($"{TvShowSeasons.Last}", $"/seasons {result.TheTvDbId} {TvShowSeasons.Last}"),
                InlineKeyboardButton.WithCallbackData($"{TvShowSeasons.All}", $"/seasons {result.TheTvDbId} {TvShowSeasons.All}"),
            }));

            Logger.LogInformation($"User '{User.UserName}' is requesting {RequestType.TvShow} '{result.Title}'. Waiting for seasons selection.");
        }
    }
}
