using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class MyCookies
    {
        public static HttpCookie CreateCookie(string Name, People people)
        {
            var newCookie = new HttpCookie(Name)
            {
                Value = Authorizated.Auth(people),
                Expires = DateTime.Now.AddDays(7)
            };

            return newCookie;
        }

        public static HttpCookie UpdateCookieSession(HttpCookie oldCookie)
        {
            People authorizationPeople = Authorizated.AuthorizationCheck(oldCookie?.Value);
            
            HttpCookie updateCookie;

            if (authorizationPeople != null)
            {
                updateCookie = new HttpCookie("hash")
                {
                    Value = oldCookie.Value,
                    Expires = DateTime.Now.AddDays(7)
                };
            }
            else
            {
                updateCookie = new HttpCookie("hash")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = null
                };
            }

            return updateCookie;
        }
    }
}