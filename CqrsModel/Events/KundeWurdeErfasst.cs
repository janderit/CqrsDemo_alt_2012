using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class KundeWurdeErfasst : Event
    {
        public Guid KundeId { get; set; }
        public string Name { get; set; }
        public string Anschrift { get; set; }
        public Guid Source { get { return KundeId; } }
    }
}
