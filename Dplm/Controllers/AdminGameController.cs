using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Dplm.Models;
using Newtonsoft.Json;
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

            List<Quest> quests = DatabaseND.GetListQuest(gameId);

            List<string> tableRows = new List<string>();
            foreach (Quest quest in quests)
            {
                tableRows.Add("<tr><td>" + quest.NumberLevel + "</td><td>" + quest.NameLevel + "</td><td><a href=\"/Administration/EditGameInformation/ViewLevelPage?gameId=" + gameId + "&lvl=" + quest.NumberLevel + "\">Редактировать</a></td></tr>");
            }

            ViewBag.TableRows = tableRows;
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

        public ActionResult ViewLevelPage()
        {
            int gameId = Int32.Parse(Request.Params["gameId"]);
            int lvl = Int32.Parse(Request.Params["lvl"]);

            ViewBag.gameId = gameId;
            ViewBag.lvl = lvl;

            return View();
        }

        public ActionResult GetLevelPage()
        {
            int gameId = Int32.Parse(Request.Params["gameId"]);
            int lvl = Int32.Parse(Request.Params["lvl"]);

            //ViewBag.gameId = gameId;
            //ViewBag.lvl = lvl;

            Quest data = DatabaseND.GetQuest(gameId, lvl);

            var jData = Json(data, JsonRequestBehavior.AllowGet);

            return jData;
        }

        public ActionResult GetAnswersOnLvl()
        {
            int questId = Int32.Parse(Request.Params["questId"]);
            List<Answer> answers = new List<Answer>();
            answers = DatabaseND.GetListAnswersOnLvl(questId);
            return Json(answers, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult UpdateLevel(Quest quest)
        {
            //Quest quest = new Quest();

            //quest.NameLevel = Request.Params["nameLevel"];      // Есть
            //quest.AuthorComment = Request.Params["authorComment"];      // Есть
            //quest.GameId = Int32.Parse(Request.Params["gameId"]);
            //quest.NumberLevel = Int32.Parse(Request.Params["lvl"]);
            //quest.TextQuest = Request.Params["textQuest"];              // Есть
            //quest.TimeOut = Int32.Parse(Request.Params["timeout"]);     // Есть
            return DatabaseND.UpdateQuest(quest)
                ? new HttpStatusCodeResult(200)
                : new HttpStatusCodeResult(500);
        }

        public ActionResult UpdateAnswersOnLvl()
        {
            int gameId = Int32.Parse(Request.Params["gameId"]);
            int lvl = Int32.Parse(Request.Params["lvl"]);

            int currentQuestId = DatabaseND.GetQuest(gameId, lvl).Id;

            List<Answer> oldAnswers = new List<Answer>();
            List<Answer> newAnswers = new List<Answer>();

            bool Flag;
            int i = 0;
            do
            {
                int id;
                int questId;
                string textAnswer;

                try
                {
                    id = Int32.Parse(Request.Params["Answer[" + i + "][Id]"]);
                }
                catch (Exception)
                {
                    id = 0;
                }

                try
                {
                    questId = Int32.Parse(Request.Params["Answer[" + i + "][QuestId]"]);
                }
                catch (Exception)
                {
                    questId = 0;
                }

                textAnswer = Request.Params["Answer[" + i + "][TextAnswer]"];

                Answer item = new Answer();
                if (textAnswer == null)
                {
                    Flag = false;
                }
                else if (textAnswer == "")
                {
                    Flag = true;
                    //Было пустое поле с ответом
                    // TODO возможно этот ответ есть в базе и игрок просто удалил ответ из поля, оставив при этом чистое поле
                }
                else if (id != 0 || questId != 0)
                {
                    item.Id = id;
                    item.QuestId = questId;
                    item.TextAnswer = Helper.RemoveExtraCharacters(textAnswer);

                    oldAnswers.Add(item);
                    Flag = true;
                }
                else
                {
                    item.Id = id;
                    item.QuestId = questId;
                    item.TextAnswer = Helper.RemoveExtraCharacters(textAnswer);

                    newAnswers.Add(item);
                    Flag = true;
                }
                i++;
            } while (Flag);

            return Helper.UpdateAnswer(oldAnswers, newAnswers, currentQuestId)
                ? new HttpStatusCodeResult(200)
                : new HttpStatusCodeResult(500);
        }

        public ActionResult DeleteAnswer()
        {
            var r = Request.Params;
            int questId, id;

            try
            {
                id = Int32.Parse(Request.Params["Answer[Id]"]);
            }
            catch (Exception)
            {
                id = 0;
            }

            try
            {
                questId = Int32.Parse(Request.Params["Answer[QuestId]"]);
            }
            catch (Exception)
            {
                questId = 0;
            }

            string textAnswer = Request.Params["Answer[TextAnswer]"];

            if (id != 0 || questId != 0)
            {
                return DatabaseND.DeleteAnswers(id)
                    ? new HttpStatusCodeResult(200)
                    : new HttpStatusCodeResult(500);
            }

            return new HttpStatusCodeResult(200);
        }
    }
}