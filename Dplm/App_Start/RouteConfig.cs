using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dplm
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new
                {
                    controller = "Start",
                    action = "StartPage",
                });

            routes.MapRoute(
                name: "Registration",
                url: "Registration/",
                defaults: new
                {
                    controller = "Start",
                    action = "RegistrationPage"
                });

            routes.MapRoute(
                name: "UserPage",
                url: "User/",
                defaults: new
                {
                    controller = "Start",
                    action = "UserPage"
                });

            routes.MapRoute(
                name: "AuthorizeUser",
                url: "AuthorizeUser/",
                defaults: new
                {
                    controller = "User",
                    action = "AuthorizeUser"
                });
        }
    }
}
