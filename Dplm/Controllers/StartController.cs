using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dplm.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult StartPage()
        {
            return View();
        }

        public ActionResult RegistrationPage()
        {
            return View();
        }

        public ActionResult UserPage()
        {
            return View();
        }
    }
}