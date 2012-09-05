using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;
using CqrsModel.Events;
using Dispositionsschnittstelle;

namespace CqrsModel.Model
{
    class Produkt : AggregateBase
    {
        private int? Verkaufspreis { get { return Konzepte.Produkt.Verkaufspreis(Historie); } }
        private int Bestand { get { return Konzepte.Produkt.Bestand(Historie); } }
        private int Zielbestand { get { return Konzepte.Produkt.Zielbestand(Historie); } }
        private int OffeneBestellungen { get { return Konzepte.Produkt.OffeneBestellungen(Historie); } }
        private string Bezeichnung { get { return Konzepte.Produkt.Bezeichnung(Historie); } }

        public void Definieren(string bezeichnung, int zielbestand)

        {
            Publish(new ProduktWurdeDefiniert {ProduktId = Id, Bezeichnung = bezeichnung});
            Publish(new ZiellagerBestandWurdeGeaendert { ProduktId = Id, Zielbestand = zielbestand });
        }

        public void Bestellen(int menge, int einkaufspreis)
        {
            Publish(new BestellungBeiLieferantGetaetigt {ProduktId = Id, Einkaufspreis = einkaufspreis, Menge = menge});
        }

        public void WareneingangVerbuchen(int menge)
        {
            Publish(new WarenlieferungGingEin {ProduktId = Id, Menge = menge});
        }

        public void VerkaufspreisVorgeben(int verkaufspreis)
        {
            Publish(new VerkaufspreisWurdeFestgesetzt{ProduktId=Id, Verkaufspreis=verkaufspreis});
            PruefeNachbestellungen();
        }

        public void ZiellagerbestandDefinieren(int zielbestand)
        {
            Publish(new ZiellagerBestandWurdeGeaendert {ProduktId = Id, Zielbestand = zielbestand});
            PruefeNachbestellungen();
        }


        public int AktuellesAngebot()
        {
            if (!Verkaufspreis.HasValue) throw new ApplicationException("Es wurde noch kein VK definiert. Bitte wenden Sie sich an Ihren Prokuristen...");
            return Verkaufspreis.Value;
        }

        public int Anfragedisposition(int menge)
        {
            return Math.Min(menge, Bestand);
        }

        public void Disponiere(Guid auftrag, string anschrift, int vk, int menge, Action<int> bestaetigung)
        {
            menge = Anfragedisposition(menge);
            if (menge>0)
            {
                Seiteneffekt(()=>new Disponent().Disponiere(Bezeichnung, menge, anschrift));
                Publish(new ProduktWurdeAusgeliefert {Anschrift=anschrift, AuftragId=auftrag, Menge=menge, ProduktId=Id, Verkaufspreis=vk});
                bestaetigung(menge);
                PruefeNachbestellungen();
            } else throw new ApplicationException("Menge nicht verfügbar");
        }

        public void PruefeNachbestellungen()
        {
            var delta = Bestand + OffeneBestellungen - Zielbestand;
            if (delta<0)
            {
                Message(new Commands.WarenlieferungBestellen {ProduktId = Id, Menge = -delta});
            }
        }
    }
}
