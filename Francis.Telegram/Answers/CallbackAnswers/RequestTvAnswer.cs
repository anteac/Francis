using Francis.Models.Notification;
using Francis.Models.Ombi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class RequestTvAnswer : CallbackAnswer
    {

        internal override bool CanProcess => Command == $"/seasons";


        public RequestTvAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            var result = await Ombi.GetTv(long.Parse(Parameters[1]));
            var seasons = (TvShowSeasons)Enum.Parse(typeof(TvShowSeasons), Parameters[2]);

            //TODO: Try to add Denied on Ombi

            result.Requested = GetStatus(result, seasons, x => x.Requested) ?? result.Requested;
            result.Approved = GetStatus(result, seasons, x => x.Approved) ?? result.Approved;
            result.Denied = GetStatus(result, seasons, x => x.Denied) ?? result.Denied;
            result.Available = GetStatus(result, seasons, x => x.Requested) ?? result.Available;

            result.SeasonRequests = seasons switch
            {
                TvShowSeasons.First => new List<SeasonRequest> { result.SeasonRequests.First() },
                TvShowSeasons.Last => new List<SeasonRequest> { result.SeasonRequests.Last() },
                _ => result.SeasonRequests,
            };

            await HandleNewQuery(result);
        }

        public static bool? GetStatus(TvSearchResult result, TvShowSeasons seasons, Func<EpisodeRequest, bool> selector)
        {
            return seasons switch
            {
                TvShowSeasons.All => result.AllEpisodes.All(selector),
                TvShowSeasons.First => result.FirstEpisodes.All(selector),
                TvShowSeasons.Last => result.LatestEpisodes.All(selector),
                _ => null,
            };
        }
    }
}
