using System;
using System.Collections.Generic;
using System.Linq;

namespace CqrsModel.Cqrs
{
    class EventStoreDatabase
    {
        private List<EventEnvelope> _historie = new List<EventEnvelope>();
        public List<EventEnvelope> Historie
        {
            get { return _historie; }
            set { _historie = value; }
        }

        public IEnumerable<EventEnvelope> All
        {
            get { return _historie.ToList(); }
        }


        public void Add(EventEnvelope envelope)
        {
            _historie.Add(envelope);
        }

        public bool Any(Guid source)
        {
            return _historie.Any(e => e.Source == source);
        }

        public int GetVersion(Guid source)
        {
            return _historie.Where(_ => _.Source == source).Max(_ => _.SourceVersion);
        }

        public IEnumerable<Event> Get(Guid source)
        {
            return _historie.Where(_ => _.Source == source).Select(_ => _.Event).ToList();
        }
    }
}