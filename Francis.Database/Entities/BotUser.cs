using System.Collections.Generic;

namespace Francis.Database.Entities
{
    public class BotUser
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string OmbiId { get; set; }

        public string PlexToken { get; set; }

        public int? RequestProgressionId { get; set; }

        public virtual List<Progression> Progressions { get; set; }

        public virtual List<WatchedItem> WatchedItems { get; set; }
    }
}
