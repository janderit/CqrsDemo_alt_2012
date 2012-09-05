using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class AuftragZeileEntfernen : Command
    {
        public Guid AuftragId { get; set; }
        public Guid ZeileId { get; set; }
    }
}
