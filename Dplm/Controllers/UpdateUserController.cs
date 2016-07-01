using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.DynamicData;
using System.Web.Http;
using Dplm.Models;
using Newtonsoft.Json.Linq;

namespace Dplm.Controllers
{
    public class UpdateUserController : ApiController
    {
        // Перенести в MVC
        //public HttpResponseMessage updatePeople(People updatePeople)
        //{
        //    if (updatePeople == null)
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }

        //    var cookie = Request.Headers.GetCookies("hash");

        //    string hash = cookie[0].Cookies[0].Value;

        //    People people = new People();
        //    Authorizated.Data.TryGetValue(hash, out people);

        //    updatePeople.Id = people.Id;

        //    if (!string.IsNullOrEmpty(updatePeople.UserPass))
        //    {
        //        updatePeople.UserPass = Helper.GetHashStringSha1(updatePeople.UserPass);
        //    }
        //    else
        //    {
        //        updatePeople.UserPass = people.UserPass;
        //    }

        //    if (DatabaseND.UpdateUser(updatePeople))
        //    {
        //        Authorizated.Data[hash] = people;
        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }

        //    return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //}
    }
}