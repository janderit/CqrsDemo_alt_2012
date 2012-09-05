using System;
using System.Collections;
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

        private readonly List<Event> _events = new List<Event>();
        private readonly List<Command> _messages = new List<Command>();
        private Repository _repo;


        [SetUp]
        public void Setup()
        {
            _events.Clear();
            _messages.Clear();
            _store = new EventStore();
            _bus = new CommandDispatcher(_store, _events.Add, _messages.Add);

            _repo = new Repository(_events.Add, a => { }, _messages.Add);

            DiContainer.Current = new DiContainer {Store = _store, CommandBus = _bus};
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
            _bus.Submit(cmd);
            Events = _events.ToList();
            Messages = _messages.ToList();
        }

        protected IEnumerable<Event> Events { get; private set; }
        protected IEnumerable<Command> Messages { get; private set; }

        protected void When<TAggregat>(Guid id, Action<TAggregat> invoker) where TAggregat : class, EventSourcedAggregate, new()
        {
            Trace.WriteLine(typeof (TAggregat) + "[" + id + "]" + ".*");

            var sut = _repo.GetEventSourced<TAggregat>(id);

            UnitOfWork.Start();
            invoker(sut);
            UnitOfWork.Commit();

            Events = _events.ToList();
            Messages = _messages.ToList();
        }

        protected void LoopMessages()
        {
            
            while (_messages.Count > 0)
            {
                _events.ForEach(e=>_store.Store(e));
                var msg = _messages.First();
                _messages.RemoveAt(0);
                When(msg);
            }
        }

    }
}
