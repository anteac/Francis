using Francis.Models.Notification;

namespace Francis.Models.Ombi
{
    public abstract class ItemSearchResult
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Trailer { get; set; }
        public string Homepage { get; set; }
        public RequestType Type { get; set; }
        public long Id { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }
        public string DeniedReason { get; set; }
        public bool Requested { get; set; }
        public long RequestId { get; set; }
        public bool Available { get; set; }
        public string PlexUrl { get; set; }
        public string EmbyUrl { get; set; }
        public string Quality { get; set; }
        public string ImdbId { get; set; }
        public string TheTvDbId { get; set; }
        public string TheMovieDbId { get; set; }
        public bool Subscribed { get; set; }
        public bool ShowSubscribe { get; set; }
    }
}
