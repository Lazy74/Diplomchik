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
            Quest quest = DatabaseND.GetQuest(1, 1);
            Game game = DatabaseND.GetGame(1);

            ViewBag.nameGame = game.NameGame;
            ViewBag.lvl = quest.NumberLevel;     // номер текущего уровня
            ViewBag.lvlLength = game.AmountLevels;  // Общее количество уровней
            ViewBag.commentAuthor = quest.AuthorComment;  // Коментарий автора
            ViewBag.quest = quest.TextQuest;    // Текст задания
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