using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public class ProduktWurdeDefiniert : Event
    {
        public Guid Source { get { return ProduktId; } }
        public Guid ProduktId { get; set; }
        public string Bezeichnung { get; set; }

        public override string ToString()
        {
            return string.Format("Produkt '{0}' wurde definiert", Bezeichnung);
        }
    }
}
