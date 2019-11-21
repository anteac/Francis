namespace Francis.Models.Notification
{
    public class Notification
    {
        public string RequestId { get; set; }
        public string RequestedUser { get; set; }
        public string Title { get; set; }
        public string RequestedDate { get; set; }
        public RequestType? Type { get; set; }
        public string TvdbId { get; set; }
        public string ImdbId { get; set; }
        public string ForeignAlbumId { get; set; }
        public string ForeignArtistId { get; set; }
        public string AdditionalInformation { get; set; }
        public string LongDate { get; set; }
        public string ShortDate { get; set; }
        public string LongTime { get; set; }
        public string ShortTime { get; set; }
        public string Overview { get; set; }
        public string Year { get; set; }
        public string EpisodesList { get; set; }
        public string SeasonsList { get; set; }
        public string PosterImage { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationUrl { get; set; }
        public string IssueDescription { get; set; }
        public string IssueCategory { get; set; }
        public string IssueStatus { get; set; }
        public string IssueSubject { get; set; }
        public string NewIssueComment { get; set; }
        public string IssueUser { get; set; }
        public string UserName { get; set; }
        public string Alias { get; set; }
        public string UserPreference { get; set; }
        public string DenyReason { get; set; }
        public string AvailableDate { get; set; }
        public NotificationType? NotificationType { get; set; }
    }
}