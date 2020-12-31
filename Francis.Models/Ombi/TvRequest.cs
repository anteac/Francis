using System;
using System.Collections.Generic;

namespace Francis.Models.Ombi
{
    public class TvRequest
    {
        public long TvDbId { get; set; }
        public string ImdbId { get; set; }
        public long QualityOverride { get; set; }
        public long RootFolder { get; set; }
        public string Overview { get; set; }
        public string Title { get; set; }
        public string PosterPath { get; set; }
        public string Background { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Status { get; set; }
        public long TotalSeasons { get; set; }
        public long Id { get; set; }

        public List<ChildRequest> ChildRequests { get; set; }
    }

    public class ChildRequest
    {
        public DateTime RequestedDate { get; set; }
    }
}
