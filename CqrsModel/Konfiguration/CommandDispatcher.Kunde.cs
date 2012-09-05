using CqrsModel.Commands;
using CqrsModel.Cqrs;
using CqrsModel.Model;

namespace CqrsModel.Konfiguration
{
    public partial class CommandDispatcher
    {
        
        public void Dispatch(KundeErfassen cmd)
        {
            _repo.CreateEventSourced<Kunde>(cmd.KundeId)
                .Erfassen(cmd.Name, cmd.Anschrift);
        }

        public void Dispatch(KundenAnschriftAendern cmd)
        {
            _repo.GetEventSourced<Kunde>(cmd.KundeId)
                .AnschriftAendern(cmd.Anschrift);
        }

    }
}
