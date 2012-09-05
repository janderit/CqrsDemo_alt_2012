using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Cqrs
{
    public class DocumentStore
    {
        private readonly Dictionary<Guid, object> _database = new Dictionary<Guid, object>();
        private readonly Dictionary<Guid, string> _types = new Dictionary<Guid, string>(); 

        public void Store(Guid id, object data, string typeannotation="")
        {
            _database.Add(id, data);
            _types.Add(id, typeannotation);
        }

        public T Get<T>(Guid id) where T : class
        {
            object result;
            _database.TryGetValue(id, out result);
            return result as T;
        }

        public void Remove(Guid id)
        {
            _database.Remove(id);
            _types.Remove(id);
        }

        public void Update(Guid id, DocumentBasedAggregate data)
        {
            _database[id] = data;
        }

        public bool Has(Guid id)
        {
            return _database.ContainsKey(id);
        }

        public IEnumerable<Guid> GetForType(string typeannotation)
        {
            return _types.Where(_ => _.Value == typeannotation).Select(_ => _.Key).ToList();
        }
    }
}
