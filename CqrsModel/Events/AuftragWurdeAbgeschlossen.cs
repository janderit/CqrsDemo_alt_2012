using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class AuftragWurdeAbgeschlossen : Event
    {
        public Guid AuftragId { get; set; }
        public Guid Source { get { return AuftragId; } }

        public override string ToString()
        {
            return "Auftrag wurde abgeschlossen";
        }
    }
}
