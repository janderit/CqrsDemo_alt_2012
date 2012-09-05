using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class KundeErfassen : Command
    {
        public KundeErfassen()
        {
            //KundeId = Guid.NewGuid();
        }

        public Guid KundeId { get; set; }
        public string Name { get; set; }
        public string Anschrift { get; set; }
    }
}
