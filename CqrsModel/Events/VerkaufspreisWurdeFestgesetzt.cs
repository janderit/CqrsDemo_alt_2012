using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class VerkaufspreisWurdeFestgesetzt : Event
    {
        public Guid Source { get { return ProduktId; } }
        public Guid ProduktId { get; set; }
        public int Verkaufspreis { get; set; }

        public override string ToString()
        {
            return string.Format("Verkaufspreis festgesetzt: {0}", Verkaufspreis);
        }
    }
}
