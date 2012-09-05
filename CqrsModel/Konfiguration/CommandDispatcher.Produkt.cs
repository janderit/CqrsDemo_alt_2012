using CqrsModel.Commands;
using CqrsModel.Cqrs;
using CqrsModel.Events;
using CqrsModel.Model;

namespace CqrsModel.Konfiguration
{
    public partial class CommandDispatcher
    {
        
        
        public void Dispatch(ProduktDefinieren cmd)
        {
            _repo.CreateEventSourced<Produkt>(cmd.ProduktId)
                .Definieren(cmd.Bezeichnung, cmd.Zielbestand);
        }

        public void Dispatch(WarenlieferungBestellen cmd)
        {
            _repo.GetEventSourced<Produkt>(cmd.ProduktId)
                .Bestellen(cmd.Menge, cmd.Einkaufspreis);
        }

        public void Dispatch(WareneingangVerbuchen cmd)
        {
            _repo.GetEventSourced<Produkt>(cmd.ProduktId)
                .WareneingangVerbuchen(cmd.Menge);
        }

        public void Dispatch(ZiellagerbestandDefinieren cmd)
        {
            _repo.GetEventSourced<Produkt>(cmd.ProduktId)
                .ZiellagerbestandDefinieren(cmd.Zielbestand);
        }

        public void Dispatch(VerkaufspreisVorgeben cmd)
        {
            _repo.GetEventSourced<Produkt>(cmd.ProduktId)
                .VerkaufspreisVorgeben(cmd.Verkaufspreis);
        }




    }
}
