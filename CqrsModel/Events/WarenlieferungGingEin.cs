using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class WarenlieferungGingEin : Event
    {
        public Guid Source { get { return ProduktId; } }
        public Guid ProduktId { get; set; }
        public int Menge { get; set; }

        public override string ToString()
        {
            return string.Format("Wareneingang {0} verbucht", Menge);
        }
    }
}
