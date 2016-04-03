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
            People people;
            var cookie = Request.Cookies["hash"];
            if (cookie != null)
            {
                string hash = cookie.Value.ToString();
                if (!Authorizated.Data.TryGetValue(hash, out people))
                {

                }
            }
            var cook = new HttpCookie("123")
            {
                Name = "test",
                Value = DateTime.Now.ToString("dd.MM.yyy"),
                Expires = DateTime.Now.AddDays(5),
            };

            Response.SetCookie(cook);
            return View();
        }

        public ActionResult RegistrationPage()
        {
            var cookie = Request.Cookies["hash"];
            return View();
        }

        public ActionResult UserPage()
        {
            var cookie = Request.Cookies["hash"];
            return View();
        }
    }
}