using System;

namespace Francis.Models.Plex
{
    public class PlexUserContainer
    {
        public PlexUser User { get; set; }
    }

    public class PlexUser
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Email { get; set; }
        public DateTime? JoinedAt { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public Uri Thumb { get; set; }
        public bool? HasPassword { get; set; }
        public string AuthToken { get; set; }
        public string AuthenticationToken { get; set; }
        public DateTime? ConfirmedAt { get; set; }
    }
}
