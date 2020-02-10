using System;

namespace Francis.Models.Ombi
{
    public class OmbiUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Alias { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public bool HasLoggedIn { get; set; }
        public string UserType { get; set; }
        public long MovieRequestLimit { get; set; }
        public long EpisodeRequestLimit { get; set; }
        public long MusicRequestLimit { get; set; }
    }
}
