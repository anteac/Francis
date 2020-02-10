using System;

namespace Francis.Models.Ombi
{
    public class MovieSearchResult : ItemSearchResult
    {
        public bool Adult { get; set; }
        public string BackdropPath { get; set; }
        public long[] GenreIds { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        public long Popularity { get; set; }
        public string PosterPath { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool Video { get; set; }
        public long VoteAverage { get; set; }
        public long VoteCount { get; set; }
        public bool AlreadyInCp { get; set; }
        public long RootPathOverride { get; set; }
        public long QualityOverride { get; set; }
        public DateTime? DigitalReleaseDate { get; set; }
    }
}
