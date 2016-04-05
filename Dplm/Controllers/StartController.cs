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
            var cookie = Request.Cookies["hash"];
            People authorizationPeople = Authorizated.AuthorizationCheck(cookie.Value);

            if (authorizationPeople != null)
            {
                cookie = new HttpCookie("hash")
                {
                    Value = Authorizated.Auth(new People()),
                    Expires = DateTime.Now.AddDays(7)
                };
            }
            else
            {
                cookie = new HttpCookie("hash")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
            }

            Response.SetCookie(cookie);
            return View();
        }

        public void AuthorizeUser(string Login, string Pass)
        {
            Login = Login.ToLower();
            if (Login == "qwe" && Pass == "asd")    // TODO здесь будет запрос в базу данных. Проверка есть ли такой user
            {
                var cookieNew = new HttpCookie("hash")
                {
                    Value = Authorizated.Auth(new People()),
                    Expires = DateTime.Now.AddDays(7)
                };

            Response.SetCookie(cookieNew);
            }
        }
    }
}