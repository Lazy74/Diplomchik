using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dplm.Models
{
    public class AuthorizatedFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies["hash"];

            // Проверка на авторизацию
            if (cookie == null || cookie.Value == "")
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            People people = new People();

            if (Authorizated.Data.Count != 0)
            {
                Authorizated.Data.TryGetValue(cookie.Value, out people);
            }

            // TODO не уверен, что people.Id == 0 это нормальная проверка
            if (people == null || people.Id == 0)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            //List<Game> games = DatabaseND.GetListGameForPeopleId(people.Id);
            //int gameId;

            //try
            //{
            //    gameId = Int32.Parse(filterContext.RequestContext.HttpContext.Request.Params["gameId"]);
            //}
            //catch (Exception)
            //{
            //    gameId = 0;
            //}

            //var a = DatabaseND.CheckAuthorInGame(people.Id, gameId);

            //var f = games.Exists(game => game.IdАuthor == people.Id);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}