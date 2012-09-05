using System;
using System.Web.Mvc;
using CqrsDemo.Models;
using CqrsModel;
using CqrsModel.Commands;
using CqrsModel.Projektionen;

namespace CqrsDemo.Controllers
{
    public class AuftraegeController : Controller
    {

        public ActionResult Index()
        {
            return View(Readmodel.Auftraege);
        }


        public ActionResult Entwerfen(Guid kundeId)
        {
            var kunde = Readmodel.Kunde(kundeId);
            var cmd = new AuftragErfassen
                          {AuftragId = Guid.NewGuid(), KundeId = kundeId, Lieferanschrift = kunde.Anschrift};
            DiContainer.Current.CommandBus.Submit(cmd);

            return RedirectToAction("Edit", new {id = cmd.AuftragId});
        }

        public ActionResult Edit(Guid id)
        {
            var auftrag = Readmodel.Auftrag(id);
            ViewBag.Auftrag = auftrag;
            ViewBag.Produkte = Readmodel.Produkte;

            ViewBag.Auftragsdaten = new AuftragsdatenBearbeiten
                                        {
                                            AuftragId = id,
                                            Lieferanschrift = auftrag.Lieferanschrift,
                                            Lieferkosten = auftrag.Lieferkosten,
                                        };

            ViewBag.NeueZeile = new AuftragZeileHinzufuegen {AuftragId = id, ZeileId = Guid.NewGuid()};

            return View();
        }

        [HttpPost]
        public ActionResult EditAuftrag(AuftragsdatenBearbeiten command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditAddZeile(AuftragZeileHinzufuegen command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Edit", new {id = command.AuftragId});
        }

        public ActionResult EditDelZeile(Guid auftrag, Guid zeile)
        {
            DiContainer.Current.CommandBus.Submit(new AuftragZeileEntfernen {AuftragId = auftrag, ZeileId = zeile});
            return RedirectToAction("Edit", new {id = auftrag});
        }


        public ActionResult Annehmen(Guid id)
        {
            var auftrag = Readmodel.Auftrag(id);
            ViewBag.Kunde = Readmodel.Kunde(auftrag.KundeId);
            return View(auftrag);
        }

        public ActionResult DoAnnehmen(Guid id)
        {
            var command = new AuftragAnnehmen {EntwurfId = id, AuftragId = Guid.NewGuid()};
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

        public ActionResult Stornieren(Guid id)
        {
            var command = new AuftragStornieren {AuftragId = id};
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Index");
        }

        public ActionResult Disponieren(Guid id)
        {
            return View(Readmodel.Auftrag(id));
        }

        [HttpPost]
        public ActionResult Disponieren(LieferungDisponieren command)
        {
            DiContainer.Current.CommandBus.Submit(command);
            return RedirectToAction("Disponieren", new {id = command.AuftragId});
        }

    }
}


