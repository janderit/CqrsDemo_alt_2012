using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class AuftragErfassen : Command
    {
        public Guid AuftragId { get; set; }
        public Guid KundeId { get; set; }
        public string Lieferanschrift { get; set; }
    }
}
