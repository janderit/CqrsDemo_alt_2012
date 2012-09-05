using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;
using CqrsModel.Events;

namespace CqrsModel.Model
{
    class Auftragszeile
    {
        private readonly Guid AuftragId;
        private readonly Guid ZeileId;
        private readonly Guid ProduktId;
        private readonly int Offen;
        private readonly int Verkaufspreis;
        private readonly string Anschrift;

        public Auftragszeile(Guid id, string anschrift, Zeile zeile, IEnumerable<AuftragWurdeTeildisponiert> events)
        {
            AuftragId = id;
            ZeileId = zeile.Id;
            ProduktId = zeile.ProduktId;
            Offen = zeile.Menge - events.Sum(_ => _.Menge);
            Verkaufspreis = zeile.Verkaufspreis;
            Anschrift = anschrift;
        }


        public void Disponiere(int menge, Func<Guid, Produkt> produkte, Action<Event> publish)
        {
            var produkt = produkte(ProduktId);

            menge = Math.Min(menge, Offen);

            var disponierbar = produkt.Anfragedisposition(menge);

            if (disponierbar > 0 && disponierbar <= menge)
            {
                Action<int> bestaetigung = (dispomenge) => publish(new AuftragWurdeTeildisponiert
                                                                       {
                                                                           AuftragId = AuftragId,
                                                                           ProduktId = ProduktId,
                                                                           Menge = dispomenge,
                                                                           Verkaufspreis = Verkaufspreis,
                                                                           ZeileId = ZeileId
                                                                       });

                produkt.Disponiere(AuftragId, Anschrift, Verkaufspreis, disponierbar, bestaetigung);
            } else throw new ApplicationException("Menge nicht verfügbar");

        }
    }
}
