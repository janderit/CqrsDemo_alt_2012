using System;
using System.Web.Mvc;
using CqrsModel;
using CqrsModel.Commands;
using CqrsModel.Projektionen;

namespace CqrsDemo.Controllers
{
    public class KundenController : Controller
    {

        public ActionResult Index()
        {
            return View(Readmodel.Kunden);
        }

        public ActionResult Create()
        {
            return View(new KundeErfassen{});
        }

        [HttpPost]
        public ActionResult Create(KundeErfassen command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

        public ActionResult AnschriftAendern(Guid id)
        {
            var kunde = Readmodel.Kunde(id);

            return View(new KundenAnschriftAendern {KundeId = id, Anschrift = kunde.Anschrift});
        }

        [HttpPost]
        public ActionResult AnschriftAendern(KundenAnschriftAendern command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

    }
}
