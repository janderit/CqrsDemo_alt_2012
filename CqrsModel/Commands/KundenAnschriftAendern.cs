using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class KundenAnschriftAendern : Command
    {
        public Guid KundeId { get; set; }
        public string Anschrift { get; set; }
    }
}
