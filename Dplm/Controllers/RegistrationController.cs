using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using Dplm.Models;

namespace Dplm.Controllers
{
    public class RegistrationController : ApiController
    {
        public HttpResponseMessage RegPeople(People people)
        {
            return DatabaseND.AddUser(people) ?
                new HttpResponseMessage(HttpStatusCode.OK) : 
                new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
