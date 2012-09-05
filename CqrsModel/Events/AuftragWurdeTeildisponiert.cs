using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class AuftragWurdeTeildisponiert : Event
    {
        public Guid Source { get { return AuftragId; } }
        public Guid AuftragId { get; set; }
        public Guid ProduktId { get; set; }
        public Guid ZeileId { get; set; }

        public int Menge { get; set; }
        public int Verkaufspreis { get; set; }
    }
}
