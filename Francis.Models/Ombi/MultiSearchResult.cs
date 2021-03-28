using Francis.Models.Notification;

namespace Francis.Models.Ombi
{
    public class MultiSearchResult
    {
        public long Id { get; set; }
        public MediaType MediaType { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string Overview { get; set; }
    }
}
