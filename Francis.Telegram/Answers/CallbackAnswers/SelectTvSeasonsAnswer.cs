using Francis.Models.Notification;
using Francis.Models.Ombi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class SelectTvSeasonsAnswer : CallbackAnswer
    {
        internal override bool CanProcess => Command == $"/seasons";


        public SelectTvSeasonsAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            var result = await Ombi.GetTv(long.Parse(Parameters[0]));
            var seasons = (TvShowSeasons)Enum.Parse(typeof(TvShowSeasons), Parameters[1]);

            //TODO: Try to add Denied on Ombi

            result.Requested = seasons switch
            {
                TvShowSeasons.All => result.AllEpisodes.All(x => x.Requested.Value),
                TvShowSeasons.First => result.FirstEpisodes.All(x => x.Requested.Value),
                TvShowSeasons.Last => result.LatestEpisodes.All(x => x.Requested.Value),
                _ => result.Requested,
            };

            result.Approved = seasons switch
            {
                TvShowSeasons.All => result.AllEpisodes.All(x => x.Approved.Value),
                TvShowSeasons.First => result.FirstEpisodes.All(x => x.Approved.Value),
                TvShowSeasons.Last => result.LatestEpisodes.All(x => x.Approved.Value),
                _ => result.Approved,
            };

            result.Available = seasons switch
            {
                TvShowSeasons.All => result.AllEpisodes.All(x => x.Available.Value),
                TvShowSeasons.First => result.FirstEpisodes.All(x => x.Available.Value),
                TvShowSeasons.Last => result.LatestEpisodes.All(x => x.Available.Value),
                _ => result.Available,
            };

            result.SeasonRequests = seasons switch
            {
                TvShowSeasons.First => new List<SeasonRequest> { result.SeasonRequests.First() },
                TvShowSeasons.Last => new List<SeasonRequest> { result.SeasonRequests.Last() },
                _ => result.SeasonRequests,
            };

            await HandleNewQuery(result);
        }
    }
}
