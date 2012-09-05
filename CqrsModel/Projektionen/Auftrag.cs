using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Projektionen
{
    public interface Auftrag
    {
        Guid Id { get; }
        Kunde Kunde { get; set; }
        Auftragsstatus Status { get; }
        Guid KundeId { get; }
        string Lieferanschrift { get; }

        IEnumerable<Auftragszeile> Zeilen { get; }

        int Verkaufswert { get; }
        int Lieferkosten { get; }
    }
}
