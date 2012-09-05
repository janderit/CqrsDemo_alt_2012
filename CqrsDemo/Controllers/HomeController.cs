using System;
using System.Linq;
using System.Web.Mvc;
using CqrsModel;
using CqrsModel.Events;

namespace CqrsDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
