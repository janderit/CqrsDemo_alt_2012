using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Projektionen
{
    public class Kunde
    {
        private readonly Guid _id;
        private readonly IEnumerable<Event> _historie;

        public Kunde(Guid id, IEnumerable<Event> historie)
        {
            _id = id;
            _historie = historie;
        }

        public Guid Id { get { return _id; } }

        public string Name { get { return _historie.OfType<Events.KundeWurdeErfasst>().Last().Name; } }
        public string Anschrift { get { return Konzepte.Kunde.Anschrift(_historie); } }
    }
}
