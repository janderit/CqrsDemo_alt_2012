using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class ProduktWurdeAusgeliefert : Event
    {
        public Guid Source { get { return ProduktId; } }
        public Guid ProduktId { get; set; }
        public Guid AuftragId { get; set; }
        public string Anschrift { get; set; }
        public int Menge { get; set; }
        public int Verkaufspreis { get; set; }
    }
}
