using System;
using System.Collections.Generic;
using System.Linq;
using CqrsModel.Cqrs;

namespace CqrsModel.Konfiguration
{
    public partial class CommandDispatcher
    {
        private readonly EventStore _store;
        private Repository _repo;

        public CommandDispatcher(EventStore store, Action<Event> publish, Action<Command> message)
        {
            _store = store;
            _repo = new Repository(
                e => UnitOfWork.OnCommit(() => publish(e)),
                UnitOfWork.OnCommit,
                m => UnitOfWork.OnCommit(() => message(m))
                );
        }

        public void Submit(Command command)
        {
            try
            {
                UnitOfWork.Start();
                Dispatch((dynamic)command);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Bei der Bearbeitung von '"+command+"' trat ein interner Fehler auf: "+ex.Message, ex);
            }
        }

        public void Dispatch(Command command)
        {
            throw new InvalidOperationException("Kein command handler registriert: "+command.GetType().Name);
        }


    }
}
