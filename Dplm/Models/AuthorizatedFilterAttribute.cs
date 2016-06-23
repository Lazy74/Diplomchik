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
            // Проверка на авторизацию
            //filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}