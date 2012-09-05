using System;
using System.Collections.Generic;
using System.Linq;

namespace CqrsModel.Cqrs
{
    public class AggregateBase: EventSourcedAggregate
    {
        private List<Event> _events;
        protected Guid Id { get; private set; }

        protected IEnumerable<Event> Historie { get { return _events; } }

        public AggregateBase()
        {
            Id = Guid.NewGuid();
            Publish = e => { };
        }

        public void SetHistory(Guid aggregateId, IEnumerable<Event> events)
        {
            if (aggregateId==Guid.Empty) throw new ArgumentException("aggregateId must be nonzero");
            Id = aggregateId;
            _events = events.ToList();
        }

        protected Action<Event> Publish  { get; private set; }

        public void SetHook(Action<Event> publisher, Action<Action> external)
        {
            if (publisher == null) throw new ArgumentNullException("publisher");
            if (external == null) throw new ArgumentNullException("external");
            Publish = publisher;
            Seiteneffekt = external;
        }

        protected Action<Action> Seiteneffekt { get; private set; }

    }
}
