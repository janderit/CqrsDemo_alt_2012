using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using CqrsDemo.App_Start;
using CqrsModel;
using CqrsModel.Cqrs;
using CqrsModel.Events;
using CqrsModel.Konfiguration;
using CqrsModel.Model;
using CqrsModel.Projektionen;

namespace CqrsDemo
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var estore = new EventStore();
            var dstore = new DocumentStore();

            Action<Command> future = cmd => DiContainer.Current.CommandBus.Submit(cmd);

            DiContainer.Current = new DiContainer
                                      {
                                          Store = estore,
                                          DocumentStore = dstore,
                                          CommandBus = new CommandDispatcher(
                                              estore,
                                              e => estore.Store(e),
                                              future
                                              ),
                                          Projektor = new Projektor(estore, dstore)
                                      };

            if (!estore.All.Any()) GenerateDemoData(estore);

        }

        private void GenerateDemoData(EventStore store)
        {
            store.Store(Produkt(Guid.NewGuid(), "Äpfel, KL. I", 1000, 28, 39));
            store.Store(Produkt(Guid.NewGuid(), "Äpfel, KL. II", 2500, 21, 29));
            store.Store(Kunde(Guid.NewGuid(), "Mustermann, Hans", "Bergstrasse 10, 91000 Allgäu"));
            store.Store(Kunde(Guid.NewGuid(), "Müller, Maria", "Dorfplatz 1, 64001 Bergstraße"));
        }

        private IEnumerable<Event> Kunde(Guid id, string name, string anschrift)
        {
            yield return new KundeWurdeErfasst {KundeId = id, Name = name, Anschrift = anschrift};
        }

        private IEnumerable<Event> Produkt(Guid id, string bezeichnung, int menge, int ek, int vk)
        {
            yield return new ProduktWurdeDefiniert {ProduktId = id, Bezeichnung = bezeichnung};
            yield return new ZiellagerBestandWurdeGeaendert() {ProduktId = id, Zielbestand = menge};
            yield return new VerkaufspreisWurdeFestgesetzt {ProduktId = id, Verkaufspreis = vk};
            yield return new BestellungBeiLieferantGetaetigt {ProduktId = id, Menge = menge, Einkaufspreis = ek};
            yield return new WarenlieferungGingEin {ProduktId = id, Menge = menge};
        }

    }
}