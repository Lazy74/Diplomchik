using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dplm.Models;

namespace Dplm.Controllers
{
    public class GameplayController : Controller
    {
        // GET: Gameplay
        public ActionResult GameplayPage()
        {
            ViewBag.nameGame = "Название игры";
            ViewBag.lvl = 5.ToString();     // номер текущего уровня
            ViewBag.lvlLength = 10.ToString();  // Общее количество уровней
            ViewBag.commentAuthor = "Здесь коментарий от автора, который поможет в игре!";  // Коментарий автора
            ViewBag.quest = "Текст интересного задания";    // Текст задания
            // придумать как выводить N количество подсказок
            return View();
        }

        //TODO этот контроллер должен принимать на вход ID игры!
        public ActionResult GamePage()
        {
            Game game = DatabaseND.GetGame(1);

            ViewBag.NameGame = game.NameGame;
            ViewBag.Id = game.Id;
            ViewBag.IdАuthor = game.IdАuthor;
            ViewBag.Sequence = game.Sequence;
            ViewBag.Distance = game.Distance;
            ViewBag.StartGame = game.StartGame;
            ViewBag.Info = game.Info;

            return View();
        }
    }
}