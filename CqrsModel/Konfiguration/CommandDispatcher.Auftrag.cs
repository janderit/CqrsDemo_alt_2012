using System;
using CqrsModel.Commands;
using CqrsModel.Cqrs;
using CqrsModel.Model;

namespace CqrsModel.Konfiguration
{
    public partial class CommandDispatcher
    {

        public void Dispatch(AuftragErfassen cmd)
        {
            _repo.CreateDocumentBased<Auftragsentwurf>(cmd.AuftragId)
                .Erfassen(cmd.KundeId, cmd.Lieferanschrift);
        }

        public void Dispatch(AuftragsdatenBearbeiten cmd)
        {
            var auftrag = _repo.GetDocumentBased<Auftragsentwurf>(cmd.AuftragId);
            if (auftrag==null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            auftrag.SetzeLieferkosten(cmd.Lieferkosten);
            auftrag.SetzeLieferanschrift(cmd.Lieferanschrift);
        }

        public void Dispatch(AuftragZeileHinzufuegen cmd)
        {
            var auftrag = _repo.GetDocumentBased<Auftragsentwurf>(cmd.AuftragId);
            if (auftrag == null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            auftrag.NeueZeile(cmd.ZeileId, cmd.ProduktId, cmd.Menge);
        }

        public void Dispatch(AuftragZeileEntfernen cmd)
        {
            var auftrag = _repo.GetDocumentBased<Auftragsentwurf>(cmd.AuftragId);
            if (auftrag == null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            auftrag.ZeileEntfernen(cmd.ZeileId);
        }

        

        public void Dispatch(AuftragAnnehmen cmd)
        {
            var entwurf = _repo.GetDocumentBased<Auftragsentwurf>(cmd.EntwurfId);
            if (entwurf == null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            var auftrag = _repo.CreateEventSourced<AngenommenerAuftrag>(cmd.AuftragId);

            auftrag.Annehmen(entwurf, _repo.GetEventSourced<Produkt>);

            UnitOfWork.OnCommit(() => _repo.DeleteDocumentBased<Auftragsentwurf>(cmd.EntwurfId));
        }

        public void Dispatch(LieferungDisponieren cmd)
        {
            var auftrag = _repo.GetEventSourced<AngenommenerAuftrag>(cmd.AuftragId);
            if (auftrag == null) throw new ApplicationException("Der Auftrag kann noch nicht disponiert werden.");
            auftrag.Disponiere(cmd.Zeile, cmd.Menge, _repo.GetEventSourced<Produkt>);
        }

        public void Dispatch(AuftragStornieren cmd)
        {
            var entwurf = _repo.GetDocumentBased<Auftragsentwurf>(cmd.AuftragId);
            if (entwurf == null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            UnitOfWork.OnCommit(() => _repo.DeleteDocumentBased<Auftragsentwurf>(cmd.AuftragId));
        }

    }
}
