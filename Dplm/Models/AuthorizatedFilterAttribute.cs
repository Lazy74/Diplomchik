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
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies["hash"];

        //    filterContext.RequestContext.HttpContext.Response.SetCookie(MyCookies.UpdateCookieSession(cookie));
        //}
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Проверка на авторизацию
            //filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}