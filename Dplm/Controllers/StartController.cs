using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dplm.Models;
using System.Web.Http;

namespace Dplm.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult StartPage()
        {
            //var cookie = Request.Cookies["hash"];
            return View();
        }

        public ActionResult RegistrationPage()
        {
            //var cookie = Request.Cookies["hash"];
            return View();
        }

        public ActionResult UserPage()
        {
            //var cookie = Request.Cookies["hash"];
            return View();
        }
    }
}