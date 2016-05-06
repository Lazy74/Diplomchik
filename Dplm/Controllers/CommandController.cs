﻿using System;
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

        //public ActionResult CommandCreatePage()
        //{
        //    return View();
        //}
    }
}