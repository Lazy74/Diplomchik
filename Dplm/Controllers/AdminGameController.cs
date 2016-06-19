using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Dplm.Models;
using Newtonsoft.Json.Linq;

namespace Dplm.Controllers
{
    [CookieFilter]
    public class AdminGameController : Controller
    {
        // TODO ВО ВСЕХ КОНТРОЛЛЕРАХ ПРОВЕРЯТЬ МОЖЕТ ЛИ ЭТОТ ПОЛЬЗОВАТЕЛЬ ИМЕТЬ АДМИНИСТРАТИВНЫЕ ПРАВА В ЭТОЙ ИГРЕ
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
                    tableRows.Add("<tr><td><a href=\"/Administration/EditGameInformation/id=" + game.Id + "\">" + game.NameGame + "</a></td><td>" + game.StartGame + "</td></tr>");
                }
            }

            ViewBag.TableRows = tableRows;

            return View();
        }

        public ActionResult ViewGamePage(string id)
        {
            int gameId = Convert.ToInt32(id);

            //var res = Json(DatabaseND.GetGame(gameId));

            //ViewBag.game = res.Data;

            ViewBag.Id = gameId;

            return View();
        }

        //[HttpGet]
        public ActionResult GetFullInfoGame(string id)
        {
            int gameId = Convert.ToInt32(id);

            Game data = DatabaseND.GetGame(gameId);

            // TODO Сделать отправку времени с сервера!!!

            //string strData = "{" +
            //                 "'test': 'test'}";

            //var jddata = Json(strData);

            //string json = @"{
            //    'Email': 'james@example.com',
            //    'Active': true,
            //    'CreatedDate': '2013-01-20T00:00:00Z',
            //}";

            var jData = Json(data, JsonRequestBehavior.AllowGet);

            return jData;
        }

        [ValidateInput(false)]
        public ActionResult UpdateInfoGame(Game game)
        {
            //byte[] buffer = Convert.FromBase64String(game.Info);
            //game.Info = Encoding.UTF8.GetString(buffer);

            // TODO переделать логику получения старой информации об игре (или же обновлять ВСЕ и полностью!!!)
            return DatabaseND.UpdateInfoGame(DatabaseND.GetGame(game.Id), game) 
                ? new HttpStatusCodeResult(200) 
                : new HttpStatusCodeResult(500);
        }
    }
}