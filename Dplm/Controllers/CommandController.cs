using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dplm.Models;

namespace Dplm.Views
{
    public class CommandController : Controller
    {
        // GET: Command
        public ActionResult AuthorizationCheck()
        {
            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            if (cookie.Value == null)
            {
                // Если была попытка попать на страницу команды без регистрации
                return new HttpUnauthorizedResult();
            }
            else
            {
                // Eсли все ок
                return new HttpStatusCodeResult(200);
            }
        }

        public ActionResult CommandPage()
        {
            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            if (cookie.Value == null)
            {
                // Если была попытка попать на страницу команды без регистрации
                return new HttpUnauthorizedResult();
            }

            //return new HttpStatusCodeResult(200);

            int teamId;
            People people = new People();

            Authorizated.Data.TryGetValue(cookie.Value, out people);

            teamId = DatabaseND.ComplianceTeamPlayer(people.Id);

            if (teamId == -1)
            {
                return View("CommandCreatePage");
            }
            Team team = DatabaseND.GetTeam(teamId);
            
            ViewBag.teamId = team.Id;
            ViewBag.teamName = team.Name;

            if (team.IdCommander == people.Id)
            {
                ViewBag.commander = "Вы командир этой команды";
            }
            else
            {
                ViewBag.commander = "Командир: " + DatabaseND.SearchPeopleById(team.IdCommander).UserLogin;
            }

            var peoplesId = DatabaseND.GetArrayPlayer(team.Id);
            var playerName = new List<string>();

            foreach (var peopleId in peoplesId)
            {
                playerName.Add(DatabaseND.SearchPeopleById(peopleId).UserLogin);
            }

            ViewBag.playerName = playerName;

            return View();
        }

        public ActionResult AddPlayer(string Login)
        {
            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            Login = Login.ToLower();

            // TODO Возвращать адекватные ответы
            People people = DatabaseND.SearchPeopleByLogin(Login);
            if (people == null)
            {
                // Если такого логина нет в базе
                return new HttpUnauthorizedResult();
            }

            int teamId = DatabaseND.ComplianceTeamPlayer(people.Id);

            if (teamId != -1)
            {
                // Игрок уже состоит в команде
                return new HttpUnauthorizedResult();
            }

            if (cookie.Value != null)
            {
                People commanderPeople = new People();
                Authorizated.Data.TryGetValue(cookie.Value, out commanderPeople);

                teamId = DatabaseND.ComplianceTeamPlayer(commanderPeople.Id);

                return DatabaseND.AddPlayerTeam(people.Id, teamId) 
                    ? new HttpStatusCodeResult(200)     // Удалось добавить
                    : new HttpUnauthorizedResult();     // не удалось добавить
            }

            // Если не удалось добавить игрока
            return new HttpUnauthorizedResult();
        }

        public ActionResult CreateTeam(string Name)
        {
            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            People people = new People();
            Authorizated.Data.TryGetValue(cookie.Value, out people);

            int teamId = DatabaseND.CreateTeam(Name, people.Id);
            if (teamId != -1)
            {
                DatabaseND.AddPlayerTeam(people.Id, teamId);
                return new HttpStatusCodeResult(200);   // Удалось добавить
            }

            return new HttpUnauthorizedResult();        // не удалось добавить
        }

        //public ActionResult CommandCreatePage()
        //{
        //    return View();
        //}
    }
}