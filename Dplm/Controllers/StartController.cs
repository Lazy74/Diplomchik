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
            Response.SetCookie(MyCookies.UpdateCookieSession(Request.Cookies["hash"]));
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
            Response.SetCookie(MyCookies.UpdateCookieSession(Request.Cookies["hash"]));
            return View();
        }

        public void AuthorizeUser(string Login, string Pass)
        {
            Login = Login.ToLower();
            if (Login == "qwe" && Pass == "asd")    // TODO здесь будет запрос в базу данных. Проверка есть ли такой user
            {
                Response.SetCookie(MyCookies.CreateCookie("hash"));
            }
        }
    }
}