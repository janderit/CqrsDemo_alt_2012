using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Commands;
using CqrsModel.Cqrs;
using CqrsModel.Events;

namespace CqrsModel.Model
{
    class Kunde : AggregateBase
    {
        public void Erfassen(string name, string anschrift)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name");
            if (anschrift == null) throw new ArgumentException("Anschrift");

            Publish(new KundeWurdeErfasst { KundeId = Id, Name = name, Anschrift = anschrift });
        }

        public void AnschriftAendern(string anschrift)
        {
            Publish(new KundenAnschriftWurdeGeaendert {KundeId = Id, Anschrift = anschrift});
        }
    }
}
