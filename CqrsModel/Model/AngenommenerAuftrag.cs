using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;
using CqrsModel.Events;

namespace CqrsModel.Model
{
    class AngenommenerAuftrag : AggregateBase, Auftrag
    {

        public void Annehmen(Auftragsentwurf entwurf, Func<Guid, Produkt> produktrepository)
        {
            var e = new AuftragWurdeAngenommen{AuftragId=Id};

            Action<Guid, string, int> setzeStammdaten = (kundeid, lieferanschrift, lieferkosten) =>
                                                            {
                                                                e.KundeId = kundeid;
                                                                e.Lieferanschrift = lieferanschrift;
                                                                e.Lieferkosten = lieferkosten;
                                                            };

            Action<Guid, Guid, int> setzeZeile = (id, produktid, menge) =>
                                                     {
                                                         var produkt = produktrepository(produktid);
                                                         var z = new Zeile
                                                                     {
                                                                         Id = id,
                                                                         ProduktId = produktid,
                                                                         Menge = menge,
                                                                         Verkaufspreis = produkt.AktuellesAngebot()
                                                                     };
                                                         e.Zeilen.Add(z);
                                                     };

            entwurf.UebergebeDaten(setzeStammdaten, setzeZeile);

            Publish(e);
        }

        private string Lieferanschrift { get { return Konzepte.Auftrag.Lieferanschrift(Historie); } }
        private int AnzahlOffenerZeilen { get { return Konzepte.Auftrag.AnzahlOffenerZeilen(Historie); } }

        private Auftragszeile Zeile(Guid id)
        {
            var init = Historie.OfType<AuftragWurdeAngenommen>().Single();
            var events = Historie.OfType<AuftragWurdeTeildisponiert>().Where(_=>_.ZeileId==id).ToList();

            return new Auftragszeile(Id, Lieferanschrift, init.Zeilen.Single(_=>_.Id==id), events);

        }

        public void Disponiere(Guid zeileid, int menge, Func<Guid, Produkt> produkte)
        {
            if (menge==0) return;
            Zeile(zeileid).Disponiere(menge, produkte, Publish);
            if (AnzahlOffenerZeilen == 0) Publish(new AuftragWurdeAbgeschlossen {AuftragId = Id});
        }
    }

}
