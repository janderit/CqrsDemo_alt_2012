using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class AuftragsdatenBearbeiten : Command
    {
        public Guid AuftragId { get; set; }
        public int Lieferkosten { get; set; }
        public string Lieferanschrift { get; set; }
    }
}
