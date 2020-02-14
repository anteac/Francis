using Francis.Models.Notification;
using System.Collections.Generic;

namespace Francis.Database.Entities
{
    public class RequestProgression : Progression
    {
        public string Search { get; set; }

        public RequestType Type { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public List<long> ExcludedIds { get; set; } = new List<long>();
    }
}
