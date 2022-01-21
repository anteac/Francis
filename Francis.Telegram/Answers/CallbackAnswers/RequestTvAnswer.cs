using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Ombi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RequestTvAnswer : RequestMediaAnswer
    {
        public override bool CanProcess => Context.Command == $"/seasons";


        public RequestTvAnswer(AnswerContext<RequestProgression> context) : base(context)
        { }


        public override async Task Execute()
        {
            var result = await Context.Ombi.GetTv(Context.Parameters[1]);
            var seasonNumber = Context.Parameters.Length > 2 ? (int?)int.Parse(Context.Parameters[2]) : null;

            //TODO: Try to add Denied on Ombi for episodes
            result.Requested = GetStatus(result, seasonNumber, x => x.Requested) ?? result.Requested;
            result.Approved = GetStatus(result, seasonNumber, x => x.Approved) ?? result.Approved;
            result.Denied = GetStatus(result, seasonNumber, x => x.Denied) ?? result.Denied;
            result.Available = GetStatus(result, seasonNumber, x => x.Available) ?? result.Available;

            if (seasonNumber != null)
            {
                var season = result.SeasonRequests.First(x => x.SeasonNumber == seasonNumber);
                result.SeasonRequests = new List<SeasonRequest> { season };
            }

            await HandleNewRequest((RequestItem)result);
        }

        public static bool? GetStatus(TvSearchResult result, int? seasonNumber, Func<EpisodeRequest, bool> selector)
        {
            if (seasonNumber == null)
            {
                return result.SeasonRequests.SelectMany(x => x.Episodes).All(selector);
            }

            return result.SeasonRequests.First(x => x.SeasonNumber == seasonNumber).Episodes.All(selector);
        }
    }
}
