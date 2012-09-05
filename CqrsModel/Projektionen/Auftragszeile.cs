using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Projektionen
{
    public class Auftragszeile
    {
        public Guid ZeileId { get; set; }
        public Guid ProduktId { get; set; }
        public Produkt Produkt { get; set; }
        public int Menge { get; set; }
        public int Disponiert { get; set; }
        public int Verkaufspreis { get; set; }
    }
}
