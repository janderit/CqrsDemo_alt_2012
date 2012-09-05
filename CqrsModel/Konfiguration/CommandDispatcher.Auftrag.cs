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
            Repository.CreateDocumentBased<Auftragsentwurf>(cmd.AuftragId)
                .Erfassen(cmd.KundeId, cmd.Lieferanschrift);
        }

        public void Dispatch(AuftragsdatenBearbeiten cmd)
        {
            var auftrag = Repository.GetDocumentBased<Auftragsentwurf>(cmd.AuftragId);
            if (auftrag==null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            auftrag.SetzeLieferkosten(cmd.Lieferkosten);
            auftrag.SetzeLieferanschrift(cmd.Lieferanschrift);
        }

        public void Dispatch(AuftragZeileHinzufuegen cmd)
        {
            var auftrag = Repository.GetDocumentBased<Auftragsentwurf>(cmd.AuftragId);
            if (auftrag == null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            auftrag.NeueZeile(cmd.ZeileId, cmd.ProduktId, cmd.Menge);
        }

        public void Dispatch(AuftragZeileEntfernen cmd)
        {
            var auftrag = Repository.GetDocumentBased<Auftragsentwurf>(cmd.AuftragId);
            if (auftrag == null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            auftrag.ZeileEntfernen(cmd.ZeileId);
        }

        

        public void Dispatch(AuftragAnnehmen cmd)
        {
            var entwurf = Repository.GetDocumentBased<Auftragsentwurf>(cmd.EntwurfId);
            if (entwurf == null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            var auftrag = Repository.CreateEventSourced<AngenommenerAuftrag>(cmd.AuftragId);

            auftrag.Annehmen(entwurf, Repository.GetEventSourced<Produkt>);

            UnitOfWork.OnCommit(()=>Repository.DeleteDocumentBased<Auftragsentwurf>(cmd.EntwurfId));
        }

        public void Dispatch(LieferungDisponieren cmd)
        {
            var auftrag = Repository.GetEventSourced<AngenommenerAuftrag>(cmd.AuftragId);
            if (auftrag == null) throw new ApplicationException("Der Auftrag kann noch nicht disponiert werden.");
            auftrag.Disponiere(cmd.Zeile, cmd.Menge, Repository.GetEventSourced<Produkt>);
        }

        public void Dispatch(AuftragStornieren cmd)
        {
            var entwurf = Repository.GetDocumentBased<Auftragsentwurf>(cmd.AuftragId);
            if (entwurf == null) throw new ApplicationException("Der Auftrag kann nicht mehr bearbeitet werden.");
            UnitOfWork.OnCommit(() => Repository.DeleteDocumentBased<Auftragsentwurf>(cmd.AuftragId));
        }

    }
}
