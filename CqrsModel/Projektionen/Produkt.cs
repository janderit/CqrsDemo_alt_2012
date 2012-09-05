using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Projektionen
{
    public class Produkt
    {
        private readonly Guid _id;
        private readonly IEnumerable<Event> _historie;

        public Produkt(Guid id, IEnumerable<Event> historie)
        {
            _id = id;
            _historie = historie;
        }

        public Guid Id { get { return _id; } }

        public string Bezeichnung { get { return _historie.OfType<Events.ProduktWurdeDefiniert>().Last(_ => _.ProduktId==_id).Bezeichnung; } }

        public int Lagerbestand { get { return Konzepte.Produkt.Bestand(_historie.Where(_ => _.Source == _id)); } }
        public int Verkaufspreis { get { return Konzepte.Produkt.Verkaufspreis(_historie)??0; } }
        public int Ziellagerbestand { get { return Konzepte.Produkt.Ziellagerbestand(_historie); } }

        public int ImZulauf
        {
            get
            {
                return
                    _historie.OfType<Events.BestellungBeiLieferantGetaetigt>().Where(_ => _.ProduktId == _id).Sum(_ => _.Menge)
                    - _historie.OfType<Events.WarenlieferungGingEin>().Where(_ => _.ProduktId == _id).Sum(_ => _.Menge);
            }
        }

    }
}
