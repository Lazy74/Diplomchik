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
    [CookieFilter]
    public class GameplayController : Controller
    {
        //public ActionResult GameplayPage()
        //{
        //    return View();
        //}


        /// <summary>
        /// Контроллер отвечающий за выдачу пользователю игровой странички
        /// </summary>
        /// <returns></returns>
        public ActionResult GameplayPage(string id)
        {
            int gameId = Convert.ToInt32(id);

            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            if (cookie.Value == null)
            {
                // TODO здесь пока пишем, что игрок не авторизован!
                //return new HttpUnauthorizedResult();        // Если команды нет в списке выдаем ошибку!
                ViewBag.Message = "Вы не авторизованны! Для игры нужно авторизоваться!";
                return View("NoGamePage");

                //return new HttpUnauthorizedResult();    // Представляет результат несанкционированного HTTP-запроса.
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

#region получаем текущий уровень и время до автоперехода

            LvlAndTime lvlDB = DatabaseND.GetPositionInGame(teamId, gameId);        // Узнаем на каком уровне сейчас команда по данным из БД

            DateTime time;

            if (lvlDB == null)
            {
                lvlDB = new LvlAndTime();
                lvlDB.numburLVL = 1;
                lvlDB.StartLVL = game.StartGame;
            }
            else
            {
                if (lvlDB.numburLVL == 0)
                {
                    ViewBag.Message = "Номер уровня вернулся 0! C этим надо что-то сделать!";
                    return View("NoGamePage");
                }
                //time = lvlDB.StartLVL;
            }
            if (lvlDB.numburLVL > game.AmountLevels)
            {
                ViewBag.Message = "Игра окончена";
                return View("NoGamePage");
            }

            LvlAndTime currentLvl = Helper.CorrectionLevel(game, lvlDB);

#endregion

            //int numberLevel = 2;

            if (currentLvl.numburLVL > game.AmountLevels || currentLvl.EndLVL < DateTime.Now)
            {
                ViewBag.Message = "Игра окончена";
                return View("NoGamePage");
            }

            Quest quest = DatabaseND.GetQuest(gameId, currentLvl.numburLVL);        // Получаем информацию о задании

            ViewBag.nameGame = game.NameGame;
            ViewBag.lvl = quest.NumberLevel;     // номер текущего уровня
            ViewBag.lvlLength = game.AmountLevels;  // Общее количество уровней
            ViewBag.commentAuthor = quest.AuthorComment;  // Коментарий автора
            ViewBag.quest = quest.TextQuest;    // Текст задания
            ViewBag.Id = game.Id;
            ViewBag.nameLevel = quest.NameLevel;

            ViewBag.TimeTransition = Helper.GetTimeTransition(lvlDB.EndLVL);
            // TODO придумать как выводить N количество подсказок
            return View();
        }

        //public ActionResult GamePage()
        //{
        //    return View();
        //}


        /// <summary>
        /// Контроллер отвечающий за отображение общей информации об игре
        /// </summary>
        /// <returns></returns>
        public ActionResult GamePage(string id)
        {
            int gameId = Convert.ToInt32(id);

            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            //int gameId = 2;     // TODO эту инфу получаем из браузера!
            Game game = DatabaseND.GetGame(gameId);

            ViewBag.NameGame = game.NameGame;
            ViewBag.Id = game.Id;
            ViewBag.IdАuthor = game.IdАuthor;
            ViewBag.Sequence = game.Sequence;
            ViewBag.Distance = game.Distance;
            ViewBag.StartGame = game.StartGame;
            ViewBag.EndGame = game.EndGame;
            ViewBag.Info = game.Info;

            List<string> tableRows = new List<string>();

            List<TeamPlay> teamPlays = DatabaseND.GetListTeamPlay(gameId);

            foreach (TeamPlay teamPlay in teamPlays)
            {
                string textPlay = teamPlay.Access ? "Приняты" : "Не приняты";
                tableRows.Add("<tr><td>" + DatabaseND.GetTeam(teamPlay.TeamId).Name + "</td><td>" + textPlay + "</td></tr>");
            }

            ViewBag.TableRows = tableRows;

            return View();
        }

        /// <summary>
        /// Контроллер отвечающий за проверку введенного ответа
        /// </summary>
        /// <param name="answer">Ответ</param>
        /// <returns></returns>
        public ActionResult AnswerСheck(string answer, string id)
        {
            int gameId = Convert.ToInt32(id);

            answer = Helper.RemoveExtraCharacters(answer);      // Удалене лишних символов

            //int gameId = 2;     // TODO эту инфу получаем из браузера!

            Game game = DatabaseND.GetGame(gameId);

            var cookie = MyCookies.UpdateCookieSession(Request.Cookies["hash"]);

            if (cookie.Value == null)
            {
                new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            People people = Authorizated.AuthorizationCheck(cookie.Value);  // получаем человека по его guid

            int teamId = DatabaseND.ComplianceTeamPlayer(people.Id);        // получаем его команду

            //int numberLVL = DatabaseND.GetPositionInGame(teamId, gameId).numburLVL;   // Получаем номер уровня на котором находится команда

            #region Узнаем где сейчас команда
            LvlAndTime lvlDB = DatabaseND.GetPositionInGame(teamId, gameId);        // Узнаем на каком уровне сейчас команда по данным из БД

            DateTime time;

            if (lvlDB == null)
            {
                lvlDB = new LvlAndTime();
                lvlDB.numburLVL = 1;
                lvlDB.StartLVL = game.StartGame;
            }
            else
            {
                if (lvlDB.numburLVL == 0)
                {
                    ViewBag.Message = "Номер уровня вернулся 0! C этим надо что-то сделать!";
                    return View("NoGamePage");
                }
                //time = lvlDB.StartLVL;
            }

            LvlAndTime currentLvl = Helper.CorrectionLevel(game, lvlDB);
            #endregion

            int questId = DatabaseND.GetQuest(gameId, lvlDB.numburLVL).Id;        //Получаем игровое задание

            if (DatabaseND.AnswerCheck(questId, answer))
            {
                DatabaseND.SetCurrentLevelForTeam(gameId, lvlDB.numburLVL, teamId, people.Id);
                return new HttpStatusCodeResult(200);
            }
            else
            {
                return new HttpStatusCodeResult(403, "incorrect unswer");
            }

            //return DatabaseND.AnswerCheck(questId, answer)
            //    ? new HttpStatusCodeResult(200)
            //    : new HttpStatusCodeResult(403, "incorrect unswer");

            //Forbidden
            //Эквивалентен HTTP - состоянию 403.Forbidden указывает, что сервер отказывается выполнять запрос.

            //NotFound
            //Эквивалентен HTTP - состоянию 404.NotFound указывает, что запрашиваемый ресурс отсутствует на сервере.
        }
    }
}