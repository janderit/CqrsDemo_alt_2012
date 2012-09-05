using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class ZiellagerBestandWurdeGeaendert : Event
    {
        public Guid ProduktId { get; set; }
        public Guid Source { get { return ProduktId; } }
        public int Zielbestand { get; set; }

        public override string ToString()
        {
            return string.Format("Ziellagerbestand auf {0} festgesetzt", Zielbestand);
        }
    }
}
