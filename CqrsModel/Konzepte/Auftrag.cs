using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;
using CqrsModel.Events;
using CqrsModel.Projektionen;

namespace CqrsModel.Konzepte
{
    public static class Auftrag
    {
        public static string Lieferanschrift(IEnumerable<Event> history)
        {
            return history.OfType<AuftragWurdeAngenommen>()
                       .Select(_ => _.Lieferanschrift).LastOrDefault()
                   ?? "";
        }

        public static int AnzahlOffenerZeilen(IEnumerable<Event> history)
        {
            var zeilen = history.OfType<AuftragWurdeAngenommen>().Single().Zeilen;

            return zeilen.Count(z => IstOffen(z, history));
        }

        private static bool IstOffen(Zeile zeile, IEnumerable<Event> history)
        {
            return history.OfType<AuftragWurdeTeildisponiert>().Where(_ => _.ZeileId == zeile.Id).Sum(_ => _.Menge) != zeile.Menge;
        }

        public static Auftragsstatus Status(IEnumerable<Event> history)
        {
            return history.OfType<AuftragWurdeAbgeschlossen>().Select(_ => (Auftragsstatus?) Auftragsstatus.Abgeschlossen).LastOrDefault()
                   ?? history.OfType<AuftragWurdeTeildisponiert>().Select(_ => (Auftragsstatus?) Auftragsstatus.Teildisponiert).LastOrDefault()
                   ?? Auftragsstatus.Offen;
        }

    }
}
