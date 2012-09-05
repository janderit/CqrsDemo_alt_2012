using System;
using System.Web.Mvc;
using CqrsModel;
using CqrsModel.Commands;
using CqrsModel.Projektionen;

namespace CqrsDemo.Controllers
{
    public class ProdukteController : Controller
    {
        //
        // GET: /Produkte/

        public ActionResult Index()
        {
            return View(Readmodel.Produkte);
        }



        public ActionResult Create()
        {
            return View(new ProduktDefinieren {ProduktId = Guid.NewGuid()});
        }

        [HttpPost]
        public ActionResult Create(ProduktDefinieren command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

        public ActionResult Bestellen(Guid id)
        {
            return View(new WarenlieferungBestellen { ProduktId = id });
        }

        [HttpPost]
        public ActionResult Bestellen(WarenlieferungBestellen command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

        public ActionResult Wareneingang(Guid id, int menge)
        {
            return View(new WareneingangVerbuchen { ProduktId = id , Menge=menge});
        }

        [HttpPost]
        public ActionResult Wareneingang(WareneingangVerbuchen command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

        public ActionResult VerkaufspreisVorgeben(Guid id)
        {
            var produkt = Readmodel.Produkt(id);
            return View(new VerkaufspreisVorgeben() { ProduktId = id, Verkaufspreis=produkt.Verkaufspreis });
        }

        [HttpPost]
        public ActionResult VerkaufspreisVorgeben(VerkaufspreisVorgeben command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

        public ActionResult ZiellagerbestandDefinieren(Guid id)
        {
            var produkt = Readmodel.Produkt(id);
            return View(new ZiellagerbestandDefinieren() { ProduktId = id, Zielbestand = produkt.Ziellagerbestand });
        }

        [HttpPost]
        public ActionResult ZiellagerbestandDefinieren(ZiellagerbestandDefinieren command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

    }
}
