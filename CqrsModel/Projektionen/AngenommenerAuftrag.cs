using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;
using CqrsModel.Events;

namespace CqrsModel.Projektionen
{
    public sealed class AngenommenerAuftrag : Auftrag
    {
        private readonly IEnumerable<Event> _historie;

        public AngenommenerAuftrag(Guid id, IEnumerable<Event> historie, Func<Guid, Produkt> produkte)
        {
            _historie = historie;
            Id = id;

            var initializer = _historie.OfType<AuftragWurdeAngenommen>().Single();

            Zeilen = initializer.Zeilen.Select(z=>ProjiziereZeile(z,produkte)).ToList();
            Verkaufswert = _historie.OfType<AuftragWurdeTeildisponiert>().Sum(_ => _.Verkaufspreis * _.Menge);
            Lieferkosten = initializer.Lieferkosten;
        }

        public Guid Id { get; private set; }
        public Auftragsstatus Status { get { return Konzepte.Auftrag.Status(_historie); } }
        public Guid KundeId { get { return _historie.OfType<AuftragWurdeAngenommen>().Single().KundeId; } }
        public string Lieferanschrift { get { return Konzepte.Auftrag.Lieferanschrift(_historie); } }

        public IEnumerable<Auftragszeile> Zeilen { get; private set; }

        private Auftragszeile ProjiziereZeile(Zeile zeile, Func<Guid, Produkt> produkte)
        {
            var produkt = produkte(zeile.ProduktId);
            var result = new Auftragszeile
                             {
                                 ZeileId = zeile.Id,
                                 Menge = zeile.Menge,
                                 ProduktId = zeile.ProduktId,
                                 Produkt = produkt,
                                 Verkaufspreis = zeile.Verkaufspreis,
                                 Disponiert = 0
                             };

            result.Disponiert = _historie.OfType<AuftragWurdeTeildisponiert>().Where(_ => _.ZeileId == zeile.Id).Sum(_ => _.Menge);
            return result;
        }

        public Kunde Kunde { get; set; } // wird extern gesetzt

        public int Verkaufswert { get; set; }
        public int Lieferkosten { get; set; }
    }
}
