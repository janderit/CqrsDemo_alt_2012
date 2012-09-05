using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class WareneingangVerbuchen : Command
    {
        public Guid ProduktId { get; set; }
        public int Menge { get; set; }
    }
}
