using Francis.Models.Notification;
using System.Collections.Generic;

namespace Francis.Database.Entities
{
    public class Progression
    {
        public int Id { get; set; }

        public long ChatId { get; set; }
    }

    public class RequestProgression : Progression
    {
        public string Search { get; set; }

        public RequestType Type { get; set; }

        public List<long> ExcludedIds { get; set; } = new List<long>();
    }
}
