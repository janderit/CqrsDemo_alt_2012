using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CqrsModel.Cqrs;
using CqrsModel.Konfiguration;
using CqrsModel.Model;
using NUnit.Framework;

namespace CqrsModel.Tests
{
    public abstract class TestBase
    {
        private CommandDispatcher _bus;
        private EventStore _store;

        [SetUp]
        public void Setup()
        {
            _store = new EventStore();
            _bus = new CommandDispatcher(_store);
            DiContainer.Current = new DiContainer { Store = _store, CommandBus = _bus };
        }

        protected void Given(params Event[] events)
        {
            events.ToList().ForEach(e=>
                                        {
                                            Trace.WriteLine(e);
                                            _store.Store(e);
                                        });

        }

        protected void When(Command cmd)
        {
            Trace.WriteLine(cmd);
            Events = _bus.Submit(cmd);
        }

        protected IEnumerable<Event> Events { get; private set; }

        protected void When<TAggregat>(Guid id, Action<TAggregat> invoker) where TAggregat : class, EventSourcedAggregate, new()
        {
            Trace.WriteLine(typeof (TAggregat) + "[" + id + "]" + ".*");
            var sut = EventSourcedRepository<TAggregat>.Get(id, e=>UnitOfWork.Publish(e.Source, e));

            UnitOfWork.Start();
            invoker(sut);
            Events = UnitOfWork.Commit();
        }

    }
}
