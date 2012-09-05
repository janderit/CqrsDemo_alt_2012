using System;
using System.Collections.Generic;
using System.Linq;
using CqrsModel.Cqrs;

namespace CqrsModel.Konfiguration
{
    public partial class CommandDispatcher
    {
        private readonly EventStore _store;

        public CommandDispatcher(EventStore store)
        {
            _store = store;
        }

        public IEnumerable<Event> Submit(Command command)
        {
            try
            {
                UnitOfWork.Start();

                Dispatch((dynamic)command);

                return UnitOfWork.Commit().ToList();
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
