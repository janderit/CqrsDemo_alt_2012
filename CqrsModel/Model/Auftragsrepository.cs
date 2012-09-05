using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;

namespace CqrsModel.Model
{
    class Auftragsrepository
    {
        public Auftrag Get(Guid id, Repository repo)
        {
            return (Auftrag)repo.GetDocumentBased<Auftragsentwurf>(id) ??
                   repo.GetEventSourced<AngenommenerAuftrag>(id);
        }
    }
}
