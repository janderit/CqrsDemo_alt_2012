using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Cqrs
{
    public interface EventSourcedAggregate
    {
        void SetHistory(Guid aggregateId, IEnumerable<Event> events);
        void SetHook(Action<Event> publisher, Action<Action> external, Action<Command> message);
    }
}
