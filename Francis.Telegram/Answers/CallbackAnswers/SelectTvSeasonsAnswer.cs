using Francis.Models;
using Francis.Models.Notification;
using Francis.Telegram.Answers;
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
        public override bool CanProcess => Context.Command == $"/chose_{MediaType.Tv}";


        public SelectTvSeasonsAnswer(AnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            var result = await Context.Ombi.GetTv(Context.Parameters[1]);

            var options = result.SeasonRequests.Select(x =>
            {
                return InlineKeyboardButton.WithCallbackData(
                    $"S{x.SeasonNumber.ToString().PadLeft(2, '0')}",
                    $"/seasons {Context.Progression.Id} {result.Id} {x.SeasonNumber}"
                );
            }).ToSublists(5).ToList();

            options.Insert(0, new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("All", $"/seasons {Context.Progression.Id} {result.Id}"),
            });

            options.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Cancel request", $"/cancel {Context.Progression.Id}"),
            });

            await Context.Bot.EditMessage(Context.Message, $"I'm about to send the request. Can you please tell me which season(s) you want?", (RequestItem)result, new InlineKeyboardMarkup(options));

            Context.Logger.LogInformation($"User {await Context.GetName()} is requesting {MediaType.Tv} '{result.Title}'. Waiting for seasons selection.");
        }
    }
}
