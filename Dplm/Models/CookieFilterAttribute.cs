using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

//using System.Web.Mvc;

namespace Dplm.Models
{
    public class CookieFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies["hash"];

            filterContext.RequestContext.HttpContext.Response.SetCookie(MyCookies.UpdateCookieSession(cookie));
        }
    }
}