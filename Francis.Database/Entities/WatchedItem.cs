using Francis.Models;
using Francis.Models.Notification;
using Newtonsoft.Json;
using System;

namespace Francis.Database.Entities
{
    public class WatchedItem
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int BotUserId { get; set; }

        public MediaType ItemType { get; set; }

        public long RequestId { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;


        public virtual BotUser BotUser { get; set; }


        public static WatchedItem From(long requestId, MediaType type, BotUser user) => new WatchedItem
        {
            RequestId = requestId,
            BotUserId = user.Id,
            ItemType = type,
        };

        public static WatchedItem From(RequestItem item, BotUser user) => From(item.RequestId, item.Type, user);
    }
}
