using Francis.Models;
using Francis.Models.Notification;

namespace Francis.Database.Entities
{
    public class WatchedItem
    {
        public int Id { get; set; }

        public long ChatId { get; set; }

        public RequestType ItemType { get; set; }

        public long RequestId { get; set; }


        public virtual BotUser BotUser { get; set; }


        public static WatchedItem From(long requestId, RequestType type, BotUser user) => new WatchedItem
        {
            RequestId = requestId,
            ChatId = user.Id,
            ItemType = type,
        };

        public static WatchedItem From(RequestItem item, BotUser user) => From(item.RequestId, item.Type, user);
    }
}
