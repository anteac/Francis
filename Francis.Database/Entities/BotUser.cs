using System.Collections.Generic;
using Newtonsoft.Json;

namespace Francis.Database.Entities
{
    public class BotUser
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        [JsonIgnore]
        public string OmbiId { get; set; }

        [JsonIgnore]
        public string PlexToken { get; set; }


        public virtual List<Progression> Progressions { get; set; }

        public virtual List<WatchedItem> WatchedItems { get; set; }
    }
}
