using System;
using System.Linq;
using System.Web.Mvc;
using CqrsModel;

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
