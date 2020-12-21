using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Francis.Toolbox.Extensions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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

            var options = result.SeasonRequests.Select(x =>
            {
                return InlineKeyboardButton.WithCallbackData(
                    $"S{x.SeasonNumber.ToString().PadLeft(2, '0')}",
                    $"/seasons {Context.Progression.Id} {result.TheTvDbId} {x.SeasonNumber}"
                );
            }).ToSublists(5).ToList();

            options.Insert(0, new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("All", $"/seasons {Context.Progression.Id} {result.TheTvDbId}"),
            });

            options.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Cancel request", $"/cancel {Context.Progression.Id}"),
            });

            await Context.Bot.EditMessage(Context.Message, $"I'm about to send the request. Can you please tell me which season(s) you want?", result, new InlineKeyboardMarkup(options));

            Context.Logger.LogInformation($"User '{Context.User.Username}' is requesting {RequestType.TvShow} '{result.Title}'. Waiting for seasons selection.");
        }
    }
}
