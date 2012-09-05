using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class BestellungBeiLieferantGetaetigt : Event
    {
        public Guid Source { get { return ProduktId; } }
        public Guid ProduktId { get; set; }
        public int Menge { get; set; }
        public int Einkaufspreis { get; set; }
    }
}
