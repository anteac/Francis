using Francis.Models.Notification;
using Francis.Models.Ombi;
using System.Collections.Generic;
using System.Linq;

namespace Francis.Models
{
    public class RequestItem
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public MediaType Type { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }
        public bool Requested { get; set; }

        public List<SeasonRequest> Seasons { get; set; }


        public RequestOmbiStatus OmbiStatus =>
            Available ? RequestOmbiStatus.Available :
            Approved ? RequestOmbiStatus.Approved :
            Denied ? RequestOmbiStatus.Denied :
            Requested ? RequestOmbiStatus.Requested :
            RequestOmbiStatus.None;


        public string AsString(string message) => $"{Title} ({Type})\n\n{message}";


        public static explicit operator RequestItem(MultiSearchResult item) => new RequestItem
        {
            Id = item.Id,
            Type = item.MediaType,
            Title = item.Title,
            Image = item.Poster,
        };

        public static explicit operator RequestItem(MovieSearchResult item) => new RequestItem
        {
            Id = item.Id,
            RequestId = item.RequestId,
            Type = MediaType.Movie,
            Title = item.Title,
            Image = item.PosterPath,
            Year = item.ReleaseDate != null && int.TryParse(item.ReleaseDate.Split('-')[0], out var result) ? result : 0,
            Requested = item.Requested,
            Approved = item.Approved,
            Denied = item.Denied,
            Available = item.Available,
        };

        public static explicit operator RequestItem(TvSearchResult item) => new RequestItem
        {
            Id = item.Id,
            RequestId = item.RequestId,
            Type = MediaType.Tv,
            Title = item.Title,
            Image = item.Images?.Original,
            Year = item.FirstAired != null && int.TryParse(item.FirstAired.Split('-')[0], out var result) ? result : 0,
            Requested = item.Requested,
            Approved = item.Approved,
            Denied = item.Denied,
            Available = item.Available,
            Seasons = item.SeasonRequests,
        };
    }
}
