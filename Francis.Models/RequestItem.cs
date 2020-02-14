using Francis.Models.Notification;
using Francis.Models.Ombi;
using System;
using System.Collections.Generic;

namespace Francis.Models
{
    public class RequestItem
    {
        public long Id { get; set; }

        public long RequestId { get; set; }

        public RequestType Type { get; set; }

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


        public string AsString(string message) => $"{Title} ({Type} - {Year})\n\n{message}";


        public static implicit operator RequestItem(ItemSearchResult item)
        {
            if (item is MovieSearchResult movieItem)
            {
                return movieItem;
            }
            if (item is TvSearchResult tvItem)
            {
                return tvItem;
            }
            throw new NotSupportedException($"'{item.GetType().Name}' is unknown and cannot be converted to a '{nameof(RequestItem)}'");
        }

        public static implicit operator RequestItem(MovieSearchResult item) => new RequestItem
        {
            Id = item.Id,
            RequestId = item.RequestId,
            Type = RequestType.Movie,
            Title = item.Title,
            Image = $"https://image.tmdb.org/t/p/w300{item.PosterPath}",
            Year = item.ReleaseDate?.Year ?? 0,
            Requested = item.Requested,
            Approved = item.Approved,
            Denied = item.Denied,
            Available = item.Available,
        };

        public static implicit operator RequestItem(TvSearchResult item) => new RequestItem
        {
            Id = item.Id,
            RequestId = item.RequestId,
            Type = RequestType.TvShow,
            Title = item.Title,
            Image = item.Banner,
            Year = item.FirstAired != null && int.TryParse(item.FirstAired.Split('-')[0], out var result) ? result : 0,
            Requested = item.Requested,
            Approved = item.Approved,
            Denied = item.Denied,
            Available = item.Available,
            Seasons = item.SeasonRequests,
        };
    }
}
