using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class ProduktDefinieren : Command
    {
        public ProduktDefinieren()
        {
            ProduktId = Guid.NewGuid();
        }

        public Guid ProduktId { get; set; }
        public string Bezeichnung { get; set; }
        public int Zielbestand { get; set; }
    }
}
