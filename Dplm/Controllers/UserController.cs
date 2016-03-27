using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Http;
using System.Web.Services.Description;
using Newtonsoft.Json.Linq;

namespace Dplm.Controllers
{
    public class UserController : ApiController
    {
        // GET: User
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public void AuthorizeUser(string login, string pass)
        {
            if (login == pass)
            {
                return;
            }
        }
    }
}