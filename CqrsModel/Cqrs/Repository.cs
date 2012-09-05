using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Cqrs
{
    public class Repository
    {

        private readonly Action<Event> _publish;
        private readonly Action<Action> _external;
        private readonly Action<Command> _message;

        public  Repository(Action<Event> publish, Action<Action> external, Action<Command> message)
        {
            _publish = publish;
            _external = external;
            _message = message;
        }


        private T TryGetFromCacheOrCache<T>(Guid id, Func<T> fallback) where T : class
        {
            if (UnitOfWork.CacheHas(id))
            {
                var t1 = UnitOfWork.GetFromCache(id) as T;
                if (t1 != null) return t1;                
            }
            var t = fallback();
            UnitOfWork.Cache(id, t);
            return t;
        }

        public T CreateEventSourced<T>(Guid id) where T : class,EventSourcedAggregate, new()
        {
            return TryGetFromCacheOrCache<T>(id, ()=>EventSourcedRepository<T>.Create(id, _publish, _external, _message));
        }

        public T CreateDocumentBased<T>(Guid id) where T : class,DocumentBasedAggregate, new()
        {
            return TryGetFromCacheOrCache<T>(id, () => DocumentBasedRepository<T>.Create(id));
        }

        public T GetEventSourced<T>(Guid id) where T : class,EventSourcedAggregate, new()
        {
            return TryGetFromCacheOrCache<T>(id, () => EventSourcedRepository<T>.Get(id, _publish, _external, _message));
        }

        public T GetDocumentBased<T>(Guid id) where T : class,DocumentBasedAggregate, new()
        {
            return TryGetFromCacheOrCache<T>(id, () => DocumentBasedRepository<T>.Get(id));
        }

        public void DeleteDocumentBased<T>(Guid id) where T : class,DocumentBasedAggregate, new()
        {
            DocumentBasedRepository<T>.Delete(id);
        }
    }
}
