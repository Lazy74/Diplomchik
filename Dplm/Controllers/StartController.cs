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

        public void AuthorizeUser(string Login, string Pass)
        {
            var cookie = Request.Cookies["hash"];
            string guid;

            if (cookie != null)
            {   
                // guid есть

                if (Authorizated.Data.ContainsKey(cookie.Values.ToString()))
                {
                    // мы знаем этот guid
                }

            }
            


            if (cookie == null)
            {
                Login = Login.ToLower();
                if (Login == "qwe" && Pass == "asd")
                {
                    guid = Guid.NewGuid().ToString("N");
                    var cookieNew = new HttpCookie("hash")
                    {
                        Name = "hash",
                        Value = guid,
                        Expires = DateTime.Now.AddDays(7),
                    };

                    Authorizated.Data.Add(guid, new People());
                    Response.SetCookie(cookieNew);
                }
                //return null;
            }
            else
            {

            }

            //return ViewBag.ToString("кук есть");
        }
    }
}