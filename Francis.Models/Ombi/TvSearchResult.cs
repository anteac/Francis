using System.Collections.Generic;

namespace Francis.Models.Ombi
{
    public class TvSearchResult
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string Title { get; set; }
        public string FirstAired { get; set; }
        public TvImage Images { get; set; }
        public bool Requested { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }
        public bool Available { get; set; }
        public List<SeasonRequest> SeasonRequests { get; set; }
    }

    public class TvImage
    {
        public string Medium { get; set; }
        public string Original { get; set; }
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
