﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult GamePage()
        {
            return View();
        }
    }
}