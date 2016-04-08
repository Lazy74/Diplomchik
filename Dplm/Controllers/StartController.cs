using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dplm.Models;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json.Linq;

namespace Dplm.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult StartPage()
        {
            Response.SetCookie(MyCookies.UpdateCookieSession(Request.Cookies["hash"]));     // Обновлениее кука (Если мы его не знаем, то идет удаление)
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

        public HttpResponseMessage AuthorizeUser(string Pass, string Login)
        {
            Login = Login.ToLower();
            People people = DatabaseND.SearchPeople(Login);     //Попытка вернуть из базы Игрока
            
            if (Login == "qwe" && Pass == "asd")    // TODO здесь будет запрос в базу данных. Проверка есть ли такой user
            {
                Response.SetCookie(MyCookies.CreateCookie("hash"));
                return new HttpResponseMessage(HttpStatusCode.OK);      // TODO это не работает! 
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);      // TODO это не работает! 
            }
        }
    }
}