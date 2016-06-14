using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dplm.Models;

namespace Dplm.Controllers
{
    [CookieFilter]
    public class AdminGameController : Controller
    {
        // GET: AdminGame
        public ActionResult HomePage()
        {
            if (Response.Cookies["hash"].Value == null)
            {
                return new HttpUnauthorizedResult();
            }

            People people = new People();

            Authorizated.Data.TryGetValue(Response.Cookies["hash"].Value, out people);

            List<string> tableRows = new List<string>();

            if (people != null)
            {
                List<Game> games = DatabaseND.GetListGameForPeopleId(people.Id);

                foreach (Game game in games)
                {
                    tableRows.Add("<tr><td><a href=\"/Game/id=" + game.Id + "\">" + game.NameGame + "</a></td><td>" + game.StartGame + "</td></tr>");
                }
            }

            ViewBag.TableRows = tableRows;

            return View();
        }
    }
}