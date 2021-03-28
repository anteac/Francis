using System;

namespace Francis.Models.Ombi
{
    public class MovieSearchResult
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public bool Requested { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }
        public bool Available { get; set; }
    }
}
