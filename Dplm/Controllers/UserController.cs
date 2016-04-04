using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web;
using System.Web.Services.Description;
using Newtonsoft.Json.Linq;

namespace Dplm.Models
{
    //public class UserController : ApiController
    public class UserController : ApiController
    {
        // GET: User
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [System.Web.Mvc.HttpGet]
        public string AuthorizeUser(string Pass, string Login)
        {
            Login = Login.ToLower();
            if (Login == "логин" && Pass=="Пароль")
            {
                string guid = Guid.NewGuid().ToString("N");
                Authorizated.Data.Add(guid, new People());

                return guid;
            }
            return null;
        }
    }
}