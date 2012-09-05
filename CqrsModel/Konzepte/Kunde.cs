using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;
using CqrsModel.Events;

namespace CqrsModel.Konzepte
{
    public static class Kunde
    {
        public static string Anschrift(IEnumerable<Event> history)
        {
            return history.OfType<KundenAnschriftWurdeGeaendert>()
                       .Select(_ => _.Anschrift).LastOrDefault()
                   ?? history.OfType<KundeWurdeErfasst>()
                          .Select(_ => _.Anschrift).SingleOrDefault()
                   ?? "";
        }

    }
}
