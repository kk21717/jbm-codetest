using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Event
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            EventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public BaseEvent(Guid eventId, DateTime createDate)
        {
            EventId = eventId;
            CreationDate = createDate;
        }

        public Guid EventId { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
