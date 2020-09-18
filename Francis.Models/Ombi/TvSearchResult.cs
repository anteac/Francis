using System.Collections.Generic;
using System.Linq;

namespace Francis.Models.Ombi
{
    public class TvSearchResult : ItemSearchResult
    {
        public string[] Aliases { get; set; }
        public string Banner { get; set; }
        public long SeriesId { get; set; }
        public string Status { get; set; }
        public string FirstAired { get; set; }
        public string Network { get; set; }
        public string NetworkId { get; set; }
        public string Runtime { get; set; }
        public string[] Genre { get; set; }
        public long? LastUpdated { get; set; }
        public string AirsDayOfWeek { get; set; }
        public string AirsTime { get; set; }
        public string Rating { get; set; }
        public long SiteRating { get; set; }
        public bool RequestAll { get; set; }
        public bool FirstSeason { get; set; }
        public bool LatestSeason { get; set; }
        public bool FullyAvailable { get; set; }
        public bool PartlyAvailable { get; set; }

        public List<SeasonRequest> SeasonRequests { get; set; }
    }

    public class SeasonRequest
    {
        public int SeasonNumber { get; set; }

        public List<EpisodeRequest> Episodes { get; set; }
    }

    public class EpisodeRequest
    {
        public int EpisodeNumber { get; set; }
        public bool Requested { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }
        public bool Available { get; set; }
    }
}
