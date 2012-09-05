using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Model
{
    class Auftragsrepository
    {
        public Auftrag Get(Guid id)
        {
            return (Auftrag)Repository.GetDocumentBased<Auftragsentwurf>(id) ??
                   Repository.GetEventSourced<AngenommenerAuftrag>(id);
        }
    }
}
