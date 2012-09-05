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
            EventSourcedRepository<Produkt>.Create(cmd.ProduktId)
                .Definieren(cmd.Bezeichnung, cmd.Zielbestand);
        }

        public void Dispatch(WarenlieferungBestellen cmd)
        {
            EventSourcedRepository<Produkt>.Create(cmd.ProduktId)
                .Bestellen(cmd.Menge, cmd.Einkaufspreis);
        }

        public void Dispatch(WareneingangVerbuchen cmd)
        {
            EventSourcedRepository<Produkt>.Create(cmd.ProduktId)
                .WareneingangVerbuchen(cmd.Menge);
        }

        public void Dispatch(ZiellagerbestandDefinieren cmd)
        {
            EventSourcedRepository<Produkt>.Create(cmd.ProduktId)
                .ZiellagerbestandDefinieren(cmd.Zielbestand);
        }

        public void Dispatch(VerkaufspreisVorgeben cmd)
        {
            EventSourcedRepository<Produkt>.Create(cmd.ProduktId)
                .VerkaufspreisVorgeben(cmd.Verkaufspreis);
        }




    }
}
