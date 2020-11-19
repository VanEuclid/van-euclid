using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VanEuclid.Controllers
{
    public class PortfolioController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

        public ActionResult Sudoku()
        {
            return View();
        }
    }
}
