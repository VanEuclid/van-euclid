using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using VanEuclid.Content;

namespace VanEuclid.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LLC()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
