using System;

namespace Francis.Models.Plex
{
    public class PlexPin
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public bool? Trusted { get; set; }
        public string ClientIdentifier { get; set; }
        public PlexLocation Location { get; set; }
        public long? ExpiresIn { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string AuthToken { get; set; }
    }
}
