using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public sealed class AuftragZeileHinzufuegen : Command
    {
        public Guid AuftragId { get; set; }
        public Guid ZeileId { get; set; }
        public Guid ProduktId { get; set; }
        public int Menge { get; set; }
    }
}
