using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Cqrs
{
    public static class Repository
    {
        private static T TryGetFromCacheOrCache<T>(Guid id, Func<T> fallback) where T : class
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

        public static T CreateEventSourced<T>(Guid id) where T : class,EventSourcedAggregate, new()
        {
            return TryGetFromCacheOrCache<T>(id, ()=>EventSourcedRepository<T>.Create(id));
        }

        public static T CreateDocumentBased<T>(Guid id) where T : class,DocumentBasedAggregate, new()
        {
            return TryGetFromCacheOrCache<T>(id, () => DocumentBasedRepository<T>.Create(id));
        }

        public static T GetEventSourced<T>(Guid id) where T : class,EventSourcedAggregate, new()
        {
            return TryGetFromCacheOrCache<T>(id, () => EventSourcedRepository<T>.Get(id));
        }

        public static T GetDocumentBased<T>(Guid id) where T : class,DocumentBasedAggregate, new()
        {
            return TryGetFromCacheOrCache<T>(id, () => DocumentBasedRepository<T>.Get(id));
        }

        public static void DeleteDocumentBased<T>(Guid id) where T : class,DocumentBasedAggregate, new()
        {
            DocumentBasedRepository<T>.Delete(id);
        }
    }
}
