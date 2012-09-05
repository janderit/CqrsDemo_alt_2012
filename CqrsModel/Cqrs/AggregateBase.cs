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
            Seiteneffekt = a => { };
            Message = cmd => { };
        }

        public void SetHistory(Guid aggregateId, IEnumerable<Event> events)
        {
            if (aggregateId==Guid.Empty) throw new ArgumentException("aggregateId must be nonzero");
            Id = aggregateId;
            _events = events.ToList();
        }

        protected void Publish(Event e)
        {
            _events.Add(e);
            _publishHook(e);
        }

        private Action<Event> _publishHook = e => { };

        public void SetHook(Action<Event> publisher, Action<Action> external, Action<Command> message)
        {
            if (publisher == null) throw new ArgumentNullException("publisher");
            if (external == null) throw new ArgumentNullException("external");
            _publishHook = publisher;
            Seiteneffekt = external;
            Message = message;
        }

        protected Action<Action> Seiteneffekt { get; private set; }
        protected Action<Command> Message { get; private set; }


    }
}
