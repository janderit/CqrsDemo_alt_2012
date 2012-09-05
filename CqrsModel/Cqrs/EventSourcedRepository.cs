using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Cqrs
{
    public static class EventSourcedRepository<TAggregat> where TAggregat:class, EventSourcedAggregate, new()
    {
        public static TAggregat Get(Guid id, Action<Event>sub =null)
        {
            sub = sub ?? (e => { });
            var store = DiContainer.Current.Store;
            var events = store.Get(id);

            if (!events.Any()) return null;

            var aggregat = new TAggregat();
            aggregat.SetHook(e =>
                                 {
                                     UnitOfWork.OnCommit(() => store.Store(e));
                                     sub(e);
                                 },
                             UnitOfWork.OnCommit
                );
            aggregat.SetHistory(id, events);

            return aggregat;
        }

        public static TAggregat Create(Guid id)
        {
            var store = DiContainer.Current.Store;

            var aggregat = new TAggregat();
            aggregat.SetHook(e =>
            {
                UnitOfWork.OnCommit(() => store.Store(e));
            },
                             UnitOfWork.OnCommit
                );
            aggregat.SetHistory(id, new List<Event>());
            return aggregat;
        }
    }
}
