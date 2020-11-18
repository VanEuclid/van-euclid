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
            ViewData["Logged"] = Globals.LoginStatus;
            return View();
        }

        public ActionResult LLC()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            Globals.LoginStatus = false;
            return View("Index");
        }

        public ActionResult SignInAttempt()
        {
            bool userNameRight = false;
            bool passwordRight = false;

            if (Request["username"].Equals("Dy"))
            {
                userNameRight = true;
            }
            if (Request["password"].Equals("Bethdem338"))
            {
                passwordRight = true;
            }

            if (userNameRight && passwordRight)
            {
                Globals.LoginStatus = true;
                return View("Index");
            }
            else
            {
                return View("SignIn");
            }
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
