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
    [CookieFilter]
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult StartPage()
        {
            //Response.SetCookie(MyCookies.UpdateCookieSession(Request.Cookies["hash"]));     // Обновлениее кука (Если мы его не знаем, то идет удаление)
            ViewBag.cookieHash = Response.Cookies["hash"].Value;

            List<Game> games = DatabaseND.GetListGames();

            List<string> tableRows = new List<string>();

            foreach (Game game in games)
            {
                tableRows.Add("<tr><td><a href=\"/Game/id=" + game.Id + "\">" + game.NameGame + "</a></td><td>" + game.StartGame + "</td></tr>");
            }

            ViewBag.TableRows = tableRows;

            return View();
        }

        public ActionResult RegPeople(People people)
        {
            people.UserPass = Helper.GetHashStringSha1(people.UserPass);

            if (DatabaseND.AddUser(people))
            {
                Response.SetCookie(MyCookies.CreateCookie("hash", people));
                return View("UserPage");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult RegistrationPage()
        {
            return View();
        }

        public ActionResult UserPage()
        {
            //var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            //Response.SetCookie(cookie);

            var cookie = Response.Cookies["hash"];

            if (cookie.Value != null)
            {
                People people = new People();
                Authorizated.Data.TryGetValue(cookie.Value, out people);

                ViewBag.userLogin = people.UserLogin;
                //ViewBag.UserPass = people.UserPass;       // а надо ли видеть пароль на странице?
                ViewBag.firstName = people.Name;
                ViewBag.lastName = people.FamiluName;
                ViewBag.id = people.Id;
                ViewBag.phoneNumber = people.PhoneNumber;
                ViewBag.email = people.Email;
                ViewBag.birthday = people.Birthday.ToString("dd MMMM yyyy");
                ViewBag.linkVK = people.LinkVK;

                return View();
            }
            else
            {
                List<Game> games = DatabaseND.GetListGames();

                List<string> tableRows = new List<string>();

                foreach (Game game in games)
                {
                    tableRows.Add("<tr><td><a href=\"/Game/id=" + game.Id + "\">" + game.NameGame + "</a></td><td>" + game.StartGame + "</td></tr>");
                }

                ViewBag.TableRows = tableRows;
                return View("StartPage");
            }
        }

        public ActionResult AuthorizeUser(string Login, string Pass)
        {
            if (Login == null || Pass == null)
            {
                // TODO возможно стоит говорить пользователю что он не ввел
                return new HttpStatusCodeResult(417);
                //return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }


            // TODO если строки логина и пароля пустые, то предыдущий if не срабатывает (ms Edge)
            Login = Login.ToLower();
            People people = DatabaseND.SearchPeopleByLogin(Login);     //Попытка вернуть из базы Игрока

            if (people == null)
            {
                return new HttpUnauthorizedResult();
            }

            List<People> peoples = Authorizated.Data.Values.ToList();
            if (peoples.Exists(people1 => people1.Id == people.Id))
            {
                return new HttpStatusCodeResult(200);
            }

            // Перевод пароля в hash
            string hashPass = Helper.GetHashStringSha1(Pass);

            if (people.UserLogin == Login && people.UserPass == hashPass)    //
            //if ("qwe" == Login && "qwe" == Pass)    // TODO здесь будет запрос в базу данных. Проверка есть ли такой user
            {
                Response.SetCookie(MyCookies.CreateCookie("hash", people));
                return new HttpStatusCodeResult(200);
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        public ActionResult Logout()
        {
            var cookie = Request.Cookies["hash"];

            if (cookie?.Value == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            return Authorizated.LogOut(cookie.Value)
                ? new HttpStatusCodeResult(HttpStatusCode.OK)
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult updateUserPage()
        {
            var cookie = Response.Cookies["hash"];

            if (cookie?.Value != null)
            {
                People people = new People();
                Authorizated.Data.TryGetValue(cookie.Value, out people);

                if (people != null)
                {
                    return View();
                }

                //ViewBag.userLogin = people.UserLogin;
                ////ViewBag.UserPass = people.UserPass;       // а надо ли видеть пароль на странице?
                //ViewBag.firstName = people.Name;
                //ViewBag.lastName = people.FamiluName;
                //ViewBag.id = people.Id;
                //ViewBag.phoneNumber = people.PhoneNumber;
                //ViewBag.email = people.Email;
                //ViewBag.birthday = people.Birthday;
                //ViewBag.linkVK = people.LinkVK;
            }
            return View("StartPage");
        }

        public ActionResult GetUserContent()
        {
            var cookie = Response.Cookies["hash"];

            if (cookie?.Value != null)
            {
                People people;
                Authorizated.Data.TryGetValue(cookie.Value, out people);

                if (people == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                people = DatabaseND.SearchPeopleById(people.Id);

                people.UserPass = "secret";

                // TODO Решение говно, но для защиты покатит
                people.Birthday = people.Birthday.AddDays(1);

                var jData = Json(people, JsonRequestBehavior.AllowGet);

                return jData;
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult UpdateUser(People newPeople)
        {
            var cookie = Response.Cookies["hash"];
            if (cookie.Value == null)
            {
                return new HttpNotFoundResult();
            }

            People people;
            if (!Authorizated.Data.TryGetValue(cookie.Value, out people))
            {
                return new HttpNotFoundResult();
            }

            newPeople.Id = people.Id;

            if (!string.IsNullOrEmpty(newPeople.UserPass))
            {
                newPeople.UserPass = Helper.GetHashStringSha1(newPeople.UserPass);
            }
            else
            {
                newPeople.UserPass = people.UserPass;
            }

            if (DatabaseND.UpdateUser(newPeople))
            {
                Authorizated.Data[cookie.Value] = newPeople;
                return new HttpStatusCodeResult(200);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}