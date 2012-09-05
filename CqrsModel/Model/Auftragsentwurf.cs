using System;
using System.Collections.Generic;
using System.Linq;
using CqrsModel.Cqrs;

namespace CqrsModel.Model
{
    public class Auftragsentwurf : DocumentBasedAggregate, Auftrag
    {
        public Guid Id { get; set; }
        public Guid KundeId { get; set; }
        public List<Auftragsentwurfzeile> Zeilen { get; set; }
        public int Lieferkosten { get; set; }
        public string Lieferanschrift { get; set; }

        public Auftragsentwurf()
        {
            Zeilen = new List<Auftragsentwurfzeile>();
        }

        public void Erfassen(Guid kundeId, string lieferanschrift)
        {
            KundeId = kundeId;
            SetzeLieferanschrift(lieferanschrift);
        }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetzeLieferkosten(int lieferkosten)
        {
            Lieferkosten = lieferkosten;
        }

        public void SetzeLieferanschrift(string lieferanschrift)
        {
            Lieferanschrift = lieferanschrift;
        }

        public void NeueZeile(Guid zeileId, Guid produktId, int menge)
        {
            Zeilen.Add(new Auftragsentwurfzeile {Id = zeileId, Menge = menge, ProduktId = produktId});
        }

        public void ZeileEntfernen(Guid zeileId)
        {
            Zeilen.Remove(Zeilen.Single(_ => _.Id == zeileId));
        }

        internal void UebergebeDaten(Action<Guid, string, int> stammdaten, Action<Guid, Guid, int> zeile)
        {
            stammdaten(KundeId, Lieferanschrift, Lieferkosten);
            Zeilen.ForEach(z => zeile(z.Id, z.ProduktId, z.Menge));
        }

    }

    public class Auftragsentwurfzeile
    {
        public Guid Id { get; set; }
        public Guid ProduktId { get; set; }
        public int Menge { get; set; }
    }
}
