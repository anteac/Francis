using Newtonsoft.Json;
using System;

namespace Francis.Database.Entities
{
    public class Progression
    {
        public int Id { get; set; }

        [JsonIgnore]
        public long BotUserId { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;


        public virtual BotUser BotUser { get; set; }
    }
}
