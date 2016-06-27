using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Dplm.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dplm.Controllers
{
    [CookieFilter]
    [AuthorizatedFilter]
    public class AdminGameController : Controller
    {
        // TODO ВО ВСЕХ КОНТРОЛЛЕРАХ ПРОВЕРЯТЬ МОЖЕТ ЛИ ЭТОТ ПОЛЬЗОВАТЕЛЬ ИМЕТЬ АДМИНИСТРАТИВНЫЕ ПРАВА В ЭТОЙ ИГРЕ
        //authorization check for id games

        /// <summary>
        /// Проверка на авторизацию по id игры
        /// </summary>
        /// <param name="hash">Cookies["hash"]</param>
        /// <param name="gameId">Id игры</param>
        /// <returns></returns>
        private bool AuthorizationCheckForGameId(string hash, int gameId)
        {
            People people = new People();

            Authorizated.Data.TryGetValue(hash, out people);

            return DatabaseND.CheckAuthorInGame(people.Id, gameId) ? true : false;
        }

        /// <summary>
        /// Главная страница в радактировании игры
        /// </summary>
        /// <returns></returns>
        public ActionResult HomePage()
        {
            //Здесь проверяем только авторизаван ли человек или нет!
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
                    tableRows.Add("<tr><td><a href=\"/Administration/EditGameInformation/id=" + game.Id + "\">" + game.NameGame + "</a></td><td>" + game.StartGame + "</td><td><a href=\"ManagementTeamPlay?gameId=" + game.Id + "\">Принять команды к участию</a></td></tr>");
                }
            }

            ViewBag.TableRows = tableRows;

            return View();
        }

        /// <summary>
        /// Страница изменения информации об игре
        /// </summary>
        /// <param name="id">Id игры</param>
        /// <returns></returns>
        public ActionResult ViewGamePage(string id)
        {
            int gameId;
            try
            {
                gameId = Convert.ToInt32(id);
            }
            catch (Exception)
            {
                gameId = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, gameId))
            {
                return new HttpUnauthorizedResult();
            }

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

        /// <summary>
        /// Для запроса на получение полной информации об игре
        /// </summary>
        /// <param name="id">Id игры</param>
        /// <returns></returns>
        public ActionResult GetFullInfoGame(string id)
        {
            int gameId;
            try
            {
                gameId = Convert.ToInt32(id);
            }
            catch (Exception)
            {
                gameId = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, gameId))
            {
                return new HttpUnauthorizedResult();
            }

            Game data = DatabaseND.GetGame(gameId);

            var jData = Json(data, JsonRequestBehavior.AllowGet);

            return jData;
        }

        /// <summary>
        /// Для запроса об обновлении информации по игре
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UpdateInfoGame(Game game)
        {
            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, game.Id))
            {
                return new HttpUnauthorizedResult();
            }

            //byte[] buffer = Convert.FromBase64String(game.Info);
            //game.Info = Encoding.UTF8.GetString(buffer);

            // TODO переделать логику получения старой информации об игре (или же обновлять ВСЕ и полностью!!!)
            return DatabaseND.UpdateInfoGame(DatabaseND.GetGame(game.Id), game)
                ? new HttpStatusCodeResult(200)
                : new HttpStatusCodeResult(500);
        }

        /// <summary>
        /// Страница редактирования уровня
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewLevelPage()
        {
            int gameId;
            try
            {
                gameId = Int32.Parse(Request.Params["gameId"]);
            }
            catch (Exception)
            {
                gameId = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, gameId))
            {
                return new HttpUnauthorizedResult();
            }

            int lvl;
            try
            {
                lvl = Int32.Parse(Request.Params["lvl"]);
            }
            catch (Exception)
            {
                lvl = 0;
            }

            ViewBag.gameId = gameId;
            ViewBag.lvl = lvl;

            return View();
        }

        /// <summary>
        /// Для запроса на получение всей информации об уровне
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLevelPage()
        {
            int gameId;
            try
            {
                gameId = Int32.Parse(Request.Params["gameId"]);
            }
            catch (Exception)
            {
                gameId = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, gameId))
            {
                return new HttpUnauthorizedResult();
            }

            int lvl;
            try
            {
                lvl = Int32.Parse(Request.Params["lvl"]);
            }
            catch (Exception)
            {
                lvl = 0;
            }

            Quest data = DatabaseND.GetQuest(gameId, lvl);

            var jData = Json(data, JsonRequestBehavior.AllowGet);

            return jData;
        }

        /// <summary>
        /// Получить список ответов на уровень
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAnswersOnLvl()
        {
            int questId;
            try
            {
                questId = Int32.Parse(Request.Params["questId"]);
            }
            catch (Exception)
            {
                questId = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, DatabaseND.GetIdGameOnQuest(questId)))
            {
                return new HttpUnauthorizedResult();
            }

            List<Answer> answers = new List<Answer>();
            answers = DatabaseND.GetListAnswersOnLvl(questId);
            return Json(answers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Для запроса на обновление информации по уровню
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UpdateLevel(Quest quest)
        {
            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, quest.GameId))
            {
                return new HttpUnauthorizedResult();
            }

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

        /// <summary>
        /// Для запроса на обновление ответов на уровень
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateAnswersOnLvl()
        {
            int gameId;
            try
            {
                gameId = Int32.Parse(Request.Params["gameId"]);
            }
            catch (Exception)
            {
                gameId = 0;
            }

            int lvl;
            try
            {
                lvl = Int32.Parse(Request.Params["lvl"]);
            }
            catch (Exception)
            {
                lvl = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, gameId))
            {
                return new HttpUnauthorizedResult();
            }

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

        /// <summary>
        /// Для удаление ответа на уровень
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteAnswer()
        {
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

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, DatabaseND.GetIdGameOnQuest(questId)))
            {
                return new HttpUnauthorizedResult();
            }

            if (id != 0 || questId != 0)
            {
                return DatabaseND.RemoveAnswers(id)
                    ? new HttpStatusCodeResult(200)
                    : new HttpStatusCodeResult(500);
            }

            // Так как данные без id, то говорим клиенту что все удалено :)
            return new HttpStatusCodeResult(200);
        }

        /// <summary>
        /// Страница управления игроющими командами
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagementTeamPlayPage()
        {
            int gameId;
            try
            {
                gameId = Int32.Parse(Request.Params["gameId"]);
            }
            catch (Exception)
            {
                gameId = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, gameId))
            {
                return new HttpUnauthorizedResult();
            }

            List<string> tableRows = new List<string>();

            List<TeamPlay> teamPlays = DatabaseND.GetListTeamPlay(gameId);

            foreach (TeamPlay teamPlay in teamPlays)
            {
                string textPlay = teamPlay.Access ? "Приняты" : "Не приняты";
                string href = teamPlay.Access ? "ManagementTeamPlay/UpdateTeamPlay?Id=" + teamPlay.Id + "&access=0" : "ManagementTeamPlay/UpdateTeamPlay?Id=" + teamPlay.Id + "&access=1";

                tableRows.Add("<tr><td>" + DatabaseND.GetTeam(teamPlay.TeamId).Name + "</td><td>" + textPlay + "</td><td><a href=\"" + href + "\">Изменить</a></td></tr>");
            }

            ViewBag.TableRows = tableRows;

            return View();
        }

        /// <summary>
        /// Для запроса на изменение решения по игре
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateTeamPlay()
        {
            // TODO Имеем только id записи, прдумать как проверить права доступа
            int id = Int32.Parse(Request.Params["Id"]);
            bool access = Int32.Parse(Request.Params["access"]) != 0;

            DatabaseND.UpdateTeamPlay(id, access);
            // Administration/ManagementTeamPlay/UpdateTeamPlay?Id=2&access=1
            return new HttpStatusCodeResult(200);
        }

        /// <summary>
        /// Добавить\удалить заявку по игре
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrRemoveApplication()
        {
            // TODO права доступа: заявку может оставить\удалить только капитан команды!
            // TODO возможно перенести в контроллер Gameplay
            int gameId;
            try
            {
                gameId = Int32.Parse(Request.Params["gameId"]);
            }
            catch (Exception)
            {
                gameId = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, gameId))
            {
                return new HttpUnauthorizedResult();
            }

            // TODO Возможно это больше не нужно
            if (Response.Cookies["hash"].Value == null)
            {
                return new HttpUnauthorizedResult();
            }

            People people = new People();

            Authorizated.Data.TryGetValue(Response.Cookies["hash"].Value, out people);

            int teamId = DatabaseND.ComplianceTeamPlayer(people.Id);

            List<TeamPlay> teamPlays = new List<TeamPlay>();
            teamPlays = DatabaseND.GetListTeamPlay(gameId);

            bool access = teamPlays.Any(t => t.TeamId == teamId);

            bool Flag;
            if (access)
            {
                Flag = DatabaseND.RemoveApplication(gameId, teamId);
            }
            else
            {
                Flag = DatabaseND.AddApplication(gameId, teamId);
            }

            return Flag
                ? new HttpStatusCodeResult(200)
                : new HttpStatusCodeResult(500);
        }

        /// <summary>
        /// Создание игры
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateGame()
        {
            if (Response.Cookies["hash"].Value == null)
            {
                return new HttpUnauthorizedResult();
            }

            People people = new People();

            Authorizated.Data.TryGetValue(Response.Cookies["hash"].Value, out people);

            int gameId = DatabaseND.CreateGame(people.Id);

            DatabaseND.AddAuthorGame(people.Id, gameId);

            return Json(gameId, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Создание уровня
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateLvl()
        {
            int gameId;
            try
            {
                gameId = Int32.Parse(Request.Params["gameId"]);
            }
            catch (Exception)
            {
                gameId = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, gameId))
            {
                return new HttpUnauthorizedResult();
            }

            int amounthLvl = DatabaseND.GetGame(gameId).AmountLevels + 1;

            // Нужен id игры, номер уровня
            // Пока берем последний номер + 1
            DatabaseND.CreateQuest(gameId, amounthLvl);

            // нужен id игры и знать добавляем или удаляем уровень
            DatabaseND.ChangeGameAmountLvl(gameId, true);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Удаление уровня
        /// </summary>
        /// <returns></returns>
        public ActionResult RemoveLvl()
        {
            int gameId;
            try
            {
                gameId = Int32.Parse(Request.Params["gameId"]);
            }
            catch (Exception)
            {
                gameId = 0;
            }

            if (!AuthorizationCheckForGameId(Request.Cookies["hash"]?.Value, gameId))
            {
                return new HttpUnauthorizedResult();
            }


            // TODO возможно стоит восстанавливать данные если хотя бы один запрос завершился с ошибкой
            int amountLevels = DatabaseND.GetGame(gameId).AmountLevels;
            if (amountLevels == 0)
            {
                return new HttpStatusCodeResult(412);   // условие ложно
            }
            int questId = DatabaseND.GetQuest(gameId, amountLevels).Id;

            DatabaseND.RemoveAllAnswersToQuest(questId);

            // Нужен id игры, номер уровня
            // Пока берем последний номер
            DatabaseND.RemoveQuest(gameId, amountLevels);

            DatabaseND.ChangeGameAmountLvl(gameId, false);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}