using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Projektionen
{
    public class Projektor
    {
        private readonly EventStore _eventstore;
        private readonly DocumentStore _docstore;

        /* Der Projektor benötigt etwas Erläuterung:
         * 
         * Für diese Demo ist es am einfachsten, die Readmodels erst auf Anfrage aus dem Eventstore zu projizieren.
         * 
         * Für ein Produktivsystem würde man die Projektionen ggf sofort bei Änerung selektiv (re)-generieren 
         * und in einem Key-Value-Store und in Ausnahmefällen in einer RDB speichern.
         * 
         * Diese vorberechneten Projektionen sind unproblematisch und lassen sich jederzeit vollständig löschen und regenerieren.
         */

        public Projektor(EventStore eventstore, DocumentStore docstore)
        {
            _eventstore = eventstore;
            _docstore = docstore;
        }

       

        


    }
}
