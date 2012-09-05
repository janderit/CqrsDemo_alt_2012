using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Projektionen
{
    public class Auftragsentwurf : Auftrag
    {
        public Auftragsentwurf(Guid auftragId, Func<Guid, Produkt> produkte)
        {
            var memento = DiContainer.Current.DocumentStore.Get<Model.Auftragsentwurf>(auftragId);
            Id = auftragId;
            KundeId = memento.KundeId;
            Lieferanschrift = memento.Lieferanschrift;
            Lieferkosten = memento.Lieferkosten;
            Zeilen = memento.Zeilen.Select(_ => new Auftragszeile
                                                    {
                                                        ZeileId = _.Id,
                                                        ProduktId = _.ProduktId,
                                                        Produkt=produkte(_.ProduktId),
                                                        Menge = _.Menge,
                                                        Disponiert=0,
                                                        Verkaufspreis = 0
                                                    }).ToList();

            Verkaufswert = Zeilen.Sum(_ => _.Menge*_.Produkt.Verkaufspreis);
        }

        public Guid Id { get; private set; }
        public Guid KundeId { get; set; }
        public Kunde Kunde { get; set; }
        public string Lieferanschrift { get; private set; }
        public IEnumerable<Auftragszeile> Zeilen { get; private set; }
        public Auftragsstatus Status { get { return Auftragsstatus.Entwurf; } }

        public int Einkaufswert { get; set; }
        public int Verkaufswert { get; set; }
        public int Lieferkosten { get; set; }
        public int Rabatt { get; set; }
    }
}
