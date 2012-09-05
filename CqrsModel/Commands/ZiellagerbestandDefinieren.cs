using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Commands
{
    public class ZiellagerbestandDefinieren : Command
    {
        public Guid ProduktId { get; set; }
        public int Zielbestand { get; set; }
    }
}
