using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Dplm.Models;

namespace Dplm.Controllers
{
    public class GameplayController : Controller
    {
        /// <summary>
        /// Контроллер отвечающий за выдачу пользователю игровой странички
        /// </summary>
        /// <returns></returns>
        public ActionResult GameplayPage()
        {
            int gameId = 2;     // TODO эту инфу получаем из браузера!

            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            if (cookie.Value == null)
            {
                return new HttpUnauthorizedResult();
            }

            People people = new People();
            Authorizated.Data.TryGetValue(cookie.Value,out people);

            int teamId = DatabaseND.ComplianceTeamPlayer(people.Id);        // Узнаем в какой команде человек
            var gameTeamIds = DatabaseND.GetGameTeamId(gameId);          // Узнаем какие команды может играть в эту игру

            var exist = gameTeamIds.Any(i => i == teamId);      // Проверяем есть ли команда игрока в списке играющих команд

            if (!exist)
            {
                // TODO если эта команда не участвует в игре, не выдавать просто ошибку
                //return new HttpUnauthorizedResult();        // Если команды нет в списке выдаем ошибку!
                ViewBag.Message = "Ваша команда не может принимать участие в игре";
                return View("NoGamePage");
            }

            Game game = DatabaseND.GetGame(gameId);      // Доп инфа для красоты отображения игрового задания
            if (DateTime.Now < game.StartGame)
            {
                ViewBag.Message = "Игра еще не началась!";
                return View("NoGamePage");
            }

            int lvlCL = GameEngine.CorrectionLevel(game);

            if (lvlCL == -1)
            {
                ViewBag.Message = "Игра окончена";
                return View("NoGamePage");
            }

            int lvlDB = DatabaseND.GetPositionInGame(teamId, gameId);        // Узнаем на каком уровне сейчас команда

            int numberLevel;
            if (lvlCL > lvlDB)
            {
                // Сделать корректировку в таблице прогресса каждой команды
                numberLevel = lvlCL;
            }
            else
            {
                numberLevel = lvlDB;
            }

            Quest quest = DatabaseND.GetQuest(gameId, numberLevel);        // Получаем информацию о задании

            ViewBag.nameGame = game.NameGame;
            ViewBag.lvl = quest.NumberLevel;     // номер текущего уровня
            ViewBag.lvlLength = game.AmountLevels;  // Общее количество уровней
            ViewBag.commentAuthor = quest.AuthorComment;  // Коментарий автора
            ViewBag.quest = quest.TextQuest;    // Текст задания
            // придумать как выводить N количество подсказок
            return View();
        }

        /// <summary>
        /// Контроллер отвечающий за отображение общей информации об игре
        /// </summary>
        /// <returns></returns>
        //TODO этот контроллер должен принимать на вход ID игры!
        public ActionResult GamePage()
        {
            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            int gameId = 2;     // TODO эту инфу получаем из браузера!
            Game game = DatabaseND.GetGame(gameId);

            ViewBag.NameGame = game.NameGame;
            ViewBag.Id = game.Id;
            ViewBag.IdАuthor = game.IdАuthor;
            ViewBag.Sequence = game.Sequence;
            ViewBag.Distance = game.Distance;
            ViewBag.StartGame = game.StartGame;
            ViewBag.Info = game.Info;

            return View();
        }

        /// <summary>
        /// Контроллер отвечающий за проверку введенного ответа
        /// </summary>
        /// <param name="answer">Ответ</param>
        /// <returns></returns>
        public ActionResult AnswerСheck(string answer)
        {
            answer = GameEngine.RemoveExtraCharacters(answer);      // Удалене лишних символов

            int gameId = 2;     // TODO эту инфу получаем из браузера!

            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            if (cookie.Value == null)
            {
                new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            People people = Authorizated.AuthorizationCheck(cookie.Value);      // получаем человека по его guid

            int teamId = DatabaseND.ComplianceTeamPlayer(people.Id);        // получаем его команду

            int numberLVL = DatabaseND.GetPositionInGame(teamId, gameId);     // Получаем номер уровня на котором находится команда

            int questId = DatabaseND.GetQuest(gameId, numberLVL).Id;

            return DatabaseND.AnswerCheck(questId, answer)
                ? new HttpStatusCodeResult(200)
                : new HttpStatusCodeResult(403, "incorrect unswer");
            

            //Forbidden
            //Эквивалентен HTTP - состоянию 403.Forbidden указывает, что сервер отказывается выполнять запрос.

            //NotFound
            //Эквивалентен HTTP - состоянию 404.NotFound указывает, что запрашиваемый ресурс отсутствует на сервере.
        }
    }
}