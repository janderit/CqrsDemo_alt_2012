using CqrsModel.Commands;
using CqrsModel.Cqrs;
using CqrsModel.Model;

namespace CqrsModel.Konfiguration
{
    public partial class CommandDispatcher
    {
        
        public void Dispatch(KundeErfassen cmd)
        {
            EventSourcedRepository<Kunde>.Create(cmd.KundeId)
                .Erfassen(cmd.Name, cmd.Anschrift);
        }

        public void Dispatch(KundenAnschriftAendern cmd)
        {
            EventSourcedRepository<Kunde>.Get(cmd.KundeId)
                .AnschriftAendern(cmd.Anschrift);
        }

    }
}
