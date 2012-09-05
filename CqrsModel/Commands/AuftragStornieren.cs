using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class AuftragStornieren : Command
    {
        public Guid AuftragId { get; set; }
    }
}
