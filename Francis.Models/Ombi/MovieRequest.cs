using System;

namespace Francis.Models.Ombi
{
    public class MovieRequest
    {
        public long? TheMovieDbId { get; set; }
        public long? IssueId { get; set; }
        public bool? Subscribed { get; set; }
        public bool? ShowSubscribe { get; set; }
        public long? RootPathOverride { get; set; }
        public long? QualityOverride { get; set; }
        public string LangCode { get; set; }
        public string ImdbId { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? DigitalReleaseDate { get; set; }
        public string Status { get; set; }
        public string Background { get; set; }
        public bool? Released { get; set; }
        public bool? DigitalRelease { get; set; }
        public string Title { get; set; }
        public bool? Approved { get; set; }
        public DateTime? MarkedAsApproved { get; set; }
        public DateTime? RequestedDate { get; set; }
        public bool? Available { get; set; }
        public DateTime? MarkedAsAvailable { get; set; }
        public string RequestedUserId { get; set; }
        public bool? Denied { get; set; }
        public DateTime? MarkedAsDenied { get; set; }
        public string DeniedReason { get; set; }
        public string RequestType { get; set; }
        public string RequestedByAlias { get; set; }
        public bool? CanApprove { get; set; }
        public long? Id { get; set; }
    }
}
