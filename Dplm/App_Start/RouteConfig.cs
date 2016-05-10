using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dplm
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Стартовая страница
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new
                {
                    controller = "Start",
                    action = "StartPage",
                });

            // Страница регистрации
            routes.MapRoute(
                name: "Registration",
                url: "Registration/",
                defaults: new
                {
                    controller = "Start",
                    action = "RegistrationPage"
                });

            // Исраница игрока
            routes.MapRoute(
                name: "UserPage",
                url: "User/",
                defaults: new
                {
                    controller = "Start",
                    action = "UserPage"
                });

            // ?
            routes.MapRoute(
                name: "AuthorizeUser",
                url: "AuthorizeUser/",
                defaults: new
                {
                    controller = "Start",
                    action = "AuthorizeUser"
                });


            // Страница обновления данных игрока
            routes.MapRoute(
                name: "updateUserPage",
                url: "User/update/",
                defaults: new
                {
                    controller = "Start",
                    action = "updateUserPage"
                });

            //// Страница информации об игре
            //routes.MapRoute(
            //    name: "GamePage",
            //    url: "Game",
            //    defaults: new
            //    {
            //        controller = "Gameplay",
            //        action = "GamePage"
            //    });

            // Страница информации об игре
            routes.MapRoute(
                name: "GamePage",
                url: "Game/id={id}",
                defaults: new
                {
                    controller = "Gameplay",
                    action = "GamePage"
                    //id = RouteParameter.Optional
                });

            // Страница процесса игры
            routes.MapRoute(
                name: "GameplayPage",
                url: "Gameplay/id={id}",
                defaults: new
                {
                    controller = "Gameplay",
                    action = "GameplayPage"
                });

            // Страница команды
            routes.MapRoute(
                name: "CreatePage",
                url: "User/Command/",
                defaults: new
                {
                    controller = "Command",
                    action = "CommandPage"
                });

            // Проверка на авторизацию 
            routes.MapRoute(
                name: "AuthorizationCheck",
                url: "User/CommandСheck/",
                defaults: new
                {
                    controller = "Command",
                    action = "AuthorizationCheck"
                });

            // Провернка ответа
            routes.MapRoute(
                name: "AnswerСheck",
                url: "GamePlay/AnswerСheck/",
                defaults: new
                {
                    controller = "Gameplay",
                    action = "AnswerСheck"
                });

            // Страница регистрации команды
            // Возможно она мне и не нужна
            //routes.MapRoute(
            //    name: "CreateCommandPage",
            //    url: "User/NewCommand",
            //    defaults: new
            //    {
            //        controller = "Command",
            //        action = "CommandCreatePage"
            //    });

            // TODO Страница изменения данных о команде
            //routes.MapRoute(
            //    name: "CreateCommandPage",
            //    url: "User/NewCommand",
            //    defaults: new
            //    {
            //        controller = "Command",
            //        action = "CommandUpdatePage"
            //    });

        }
    }
}
