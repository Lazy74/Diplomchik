using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dplm.Models;
using Newtonsoft.Json.Linq;

namespace Dplm.Controllers
{
    public class UpdateUserController : ApiController
    {
        public HttpResponseMessage updatePeople(People updatePeople)
        {
            if (updatePeople == null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            var cookie = Request.Headers.GetCookies("hash");

            string hash = cookie[0].Cookies[0].Value;

            People people = new People();
            Authorizated.Data.TryGetValue(hash, out people);


            return DatabaseND.UpdateUser(people, updatePeople)?
                new HttpResponseMessage(HttpStatusCode.OK) :
                new HttpResponseMessage(HttpStatusCode.BadRequest);

            //return DatabaseND.AddUser(people) ?
            //    new HttpResponseMessage(HttpStatusCode.OK) :
            //    new HttpResponseMessage(HttpStatusCode.BadRequest);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}