using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class LieferungDisponieren : Command
    {
        public Guid AuftragId { get; set; }
        public Guid Zeile { get; set; }
        public int Menge { get; set; }
    }
}
