using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Events
{
    public sealed class AuftragWurdeAngenommen : Event
    {
        public AuftragWurdeAngenommen()
        {
            Zeilen = new List<Zeile>();
        }

        public Guid AuftragId { get; set; }
        public Guid KundeId { get; set; }
        public Guid Source { get { return AuftragId; } }
        public string Lieferanschrift { get; set; }
        public int Lieferkosten { get; set; }

        public int Netto { get { return Zeilen.Sum(_ => _.Verkaufspreis*_.Menge); } }
        public int Brutto { get { return Netto + Lieferkosten; } }

        public List<Zeile> Zeilen { get; set; } 
    }

    public struct Zeile
    {
        public Guid Id { get; set; }
        public Guid ProduktId { get; set; }
        public int Menge { get; set; }
        public int Verkaufspreis { get; set; }
    }
}
