using System.Collections.Generic;
using Newtonsoft.Json;

namespace Francis.Database.Entities
{
    public class BotUser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        [JsonIgnore]
        public long TelegramId { get; set; }


        public virtual List<Progression> Progressions { get; set; }

        public virtual List<WatchedItem> WatchedItems { get; set; }
    }
}
