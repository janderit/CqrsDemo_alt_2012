using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Konzepte
{
    public static class Produkt
    {
        public static int? Verkaufspreis(IEnumerable<Event> history)
        {
            return history.OfType<Events.VerkaufspreisWurdeFestgesetzt>().Select(_ => (int?) _.Verkaufspreis).LastOrDefault();
        }

        public static string Bezeichnung(IEnumerable<Event> history)
        {
            return history.OfType<Events.ProduktWurdeDefiniert>().Select(_ => _.Bezeichnung).LastOrDefault() ?? "";
        }

        public static int Ziellagerbestand(IEnumerable<Event> history)
        {
            return history.OfType<Events.ZiellagerBestandWurdeGeaendert>().Select(_ => (int?) _.Zielbestand).LastOrDefault() ?? 0;
        }

        public static int Bestand(IEnumerable<Event> history)
        {
            return
                history.OfType<Events.WarenlieferungGingEin>().Sum(_ => _.Menge)
                - history.OfType<Events.ProduktWurdeAusgeliefert>().Sum(_ => _.Menge);
        }
    }
}
