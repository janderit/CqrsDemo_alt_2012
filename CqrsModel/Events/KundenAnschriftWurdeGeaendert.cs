using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class KundenAnschriftWurdeGeaendert : Event
    {
        public Guid KundeId { get; set; }
        public string Anschrift { get; set; }
        public Guid Source { get { return KundeId; } }
    }
}
