using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class MyCookies
    {
        public static HttpCookie CreateCookie(string Name)
        {
            var newCookie = new HttpCookie(Name)
            {
                Value = Authorizated.Auth(new People()),
                Expires = DateTime.Now.AddDays(7)
            };

            return newCookie;
        }

        public static HttpCookie UpdateCookieSession(HttpCookie oldCookie)
        {
            People authorizationPeople = Authorizated.AuthorizationCheck(oldCookie?.Value);
            //People authorizationPeople = null;

            HttpCookie updateCookie;

            if (authorizationPeople != null)
            {
                updateCookie = new HttpCookie("hash")
                {
                    Value = Authorizated.Auth(new People()),
                    Expires = DateTime.Now.AddDays(7)
                };
            }
            else
            {
                updateCookie = new HttpCookie("hash")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
            }

            return updateCookie;
        }
    }
}