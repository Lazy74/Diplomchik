using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Services.Description;
using Newtonsoft.Json.Linq;

namespace Dplm.Models
{
    //public class UserController : ApiController
    public class UserController : ApiController
    {
        //public HttpResponseMessage AuthorizeUser(string Pass, string Login)
        //{
        //    Login = Login.ToLower();

        //    if (Login == "qwe" && Pass == "asd")    // TODO здесь будет запрос в базу данных. Проверка есть ли такой user
        //    {
        //        //Response.SetCookie(MyCookies.CreateCookie("hash"));
        //        //return new HttpResponseMessage(HttpStatusCode.OK);
        //        var response = Request.CreateResponse(HttpStatusCode.OK);
        //        response.Headers.AddCookies(new MyCookies.CreateCookie("hash"))
        //    }
        //    else
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
        //}
    }
}