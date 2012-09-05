using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Cqrs
{
    public static class DocumentBasedRepository<TAggregat> where TAggregat : class, DocumentBasedAggregate, new()
    {
        public static TAggregat Get(Guid id)
        {
            var aggregat = DiContainer.Current.DocumentStore.Get<TAggregat>(id);
            UnitOfWork.Register(() => true, ()=>DiContainer.Current.DocumentStore.Update(id, aggregat));
            return aggregat;
        }

        public static TAggregat Create(Guid id)
        {
            var agg = new TAggregat();
            agg.SetId(id);
            UnitOfWork.Register(() => true, ()=>DiContainer.Current.DocumentStore.Store(id, agg, agg.GetType().Name));
            return agg;
        }

        public static void Delete(Guid id)
        {
            DiContainer.Current.DocumentStore.Remove(id);
        }
    }
}
