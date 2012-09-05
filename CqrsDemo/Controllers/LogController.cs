using System.Linq;
using System.Web.Mvc;
using CqrsDemo.Models;
using CqrsModel;
using CqrsModel.Cqrs;

namespace CqrsDemo.Controllers
{
    public class LogController : Controller
    {
        //
        // GET: /Log/

        public ActionResult Index()
        {
            return View(DiContainer.Current.Store.AllEnvelopes.Select(CreateLogLine).ToList());
        }

        private LogLine CreateLogLine(EventEnvelope envelope)
        {
            return new LogLine {Lfd = envelope.Inkrement, Zeit = envelope.Zeit, Ereignis = envelope.Event.ToString(), Source=envelope.Source.ToString(), Version=envelope.SourceVersion};
        }

    }
}
