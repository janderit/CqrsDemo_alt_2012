using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Cqrs
{
    public class EventStore
    {
        private int _inkrement = 0;
        private readonly EventStoreDatabase _db = new EventStoreDatabase();

        public void Store(params Event[] e) { Store(e.ToList()); }

        public void Store(IEnumerable<Event> e)
        {
            foreach (var evt in e)
            {
                var source = evt.Source;

                var vnext =
                    _db.Any(source)
                        ? _db.GetVersion(source) + 1
                        : 0;

                var envelope = new EventEnvelope
                                   {
                                       Zeit=DateTime.Now,
                                       Source = source,
                                       Event = evt,
                                       Inkrement = _inkrement++,
                                       SourceVersion = vnext
                                   };
                _db.Add(envelope);
            }
        }

        public IEnumerable<Event> All { get { return _db.All.Select(_ => _.Event); } }

        public IEnumerable<EventEnvelope> AllEnvelopes
        {
            get { return _db.All; }
        }

        public IEnumerable<Event> Get(Guid source)
        {
            return _db.Get(source);
        }

    }
}
