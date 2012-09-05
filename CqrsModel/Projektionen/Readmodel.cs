using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Projektionen
{
    public static class Readmodel
    {
        public static IEnumerable<Produkt> Produkte{get { return DiContainer.Current.Projektor.Produkte; }}
        public static Produkt Produkt(Guid id){ return DiContainer.Current.Projektor.Produkt(id); }

        public static IEnumerable<Kunde> Kunden { get { return DiContainer.Current.Projektor.Kunden; } }
        public static Kunde Kunde(Guid id) { return DiContainer.Current.Projektor.Kunde(id); }

       
    }
}
