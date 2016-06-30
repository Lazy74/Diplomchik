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

            // Регистрация
            routes.MapRoute(
                name: "RegPeople",
                url: "Registration/NewPeople",
                defaults: new
                {
                    controller = "Start",
                    action = "RegPeople",
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

            // Авторизация
            routes.MapRoute(
                name: "AuthorizeUser",
                url: "AuthorizeUser/",
                defaults: new
                {
                    controller = "Start",
                    action = "AuthorizeUser"
                });

            // Разлогироваться
            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new
                {
                    controller = "Start",
                    action = "Logout",
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

            // Страница информации об игре
            routes.MapRoute(
                name: "GetUserContent",
                url: "User/update/GetUserContent",
                defaults: new
                {
                    controller = "Start",
                    action = "GetUserContent"
                });

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

            // Создание команды
            routes.MapRoute(
                name: "CreateCommand",
                url: "User/Command/CreateTeam/",
                defaults: new
                {
                    controller = "Command",
                    action = "CreateTeam"
                });

            // Добавление игрока в команду
            routes.MapRoute(
                name: "AddPlayer",
                url: "User/Command/AddPlayer/",
                defaults: new
                {
                    controller = "Command",
                    action = "AddPlayer"
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
                url: "GamePlay/AnswerСheck/id={id}",
                defaults: new
                {
                    controller = "Gameplay",
                    action = "AnswerСheck"
                });

            // Администраторская игр (начальная страница)
            routes.MapRoute(
                name: "Administration",
                url: "Administration",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "HomePage"
                });

            // Страница редактирования описания игры (начальная страница)
            routes.MapRoute(
                name: "EditGameInformation",
                url: "Administration/EditGameInformation/id={id}",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "ViewGamePage"
                });

            // Отправить полную информацию об игре
            routes.MapRoute(
                name: "GetFullInfoGame",
                url: "Administration/GetFullInfoGame/id={id}",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "GetFullInfoGame"
                });

            // Получить обновленную информацию об игре 
            routes.MapRoute(
                name: "UpdateInfoGame",
                url: "Administration/UpdateInfoGame",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "UpdateInfoGame"
                });

            // Страница редактирования уровня
            routes.MapRoute(
                name: "ViewLevelPage",
                url: "Administration/EditGameInformation/ViewLevelPage",
                //url: "Administration/EditGameInformation/ViewLevelPage?gameId={gameId}&lvl={lvl}",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "ViewLevelPage"
                });

            // Для запроса на получение информации об уровне
            routes.MapRoute(
                name: "GetLevelPage",
                url: "Administration/EditGameInformation/GetLevelPage",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "GetLevelPage"
                });

            // Для запроса на получение списка ответов
            routes.MapRoute(
                name: "GetAnswersOnLvl",
                url: "Administration/EditGameInformation/GetAnswersOnLvl",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "GetAnswersOnLvl"
                });

            // Для запроса на обновление уровня
            routes.MapRoute(
                name: "UpdateLevel",
                url: "Administration/EditGameInformation/UpdateLevel",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "UpdateLevel"
                });

            // Для запроса на обновление списка ответов
            routes.MapRoute(
                name: "UpdateAnswersOnLvl",
                url: "Administration/EditGameInformation/UpdateAnswersOnLvl",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "UpdateAnswersOnLvl"
                });


            // Для запроса на удаление ответа
            routes.MapRoute(
                name: "DeleteAnswer",
                url: "Administration/EditGameInformation/DeleteAnswer",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "DeleteAnswer"
                });

            // Страница управления игроющими командами
            routes.MapRoute(
                name: "ManagementTeamPlayPage",
                url: "Administration/ManagementTeamPlay",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "ManagementTeamPlayPage"
                });

            // Для изменения записи об игроющей команде
            routes.MapRoute(
                name: "UpdateTeamPlay",
                url: "Administration/ManagementTeamPlay/UpdateTeamPlay",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "UpdateTeamPlay"
                });

            // Для запроса на добавление или удаление заявки
            routes.MapRoute(
                name: "AddOrRemoveApplication",
                url: "Administration/ManagementTeamPlay/Application",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "AddOrRemoveApplication"
                });

            // Для запроса на создание игры
            routes.MapRoute(
                name: "CreateGame",
                url: "Administration/CreateGame",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "CreateGame"
                });

            // Для запроса на создание уровня
            routes.MapRoute(
                name: "CreateLvl",
                url: "Administration/EditGameInformation/CreateLvl",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "CreateLvl"
                });

            // Для запроса на удаление уровня
            routes.MapRoute(
                name: "RemoveLvl",
                url: "Administration/EditGameInformation/RemoveLvl",
                defaults: new
                {
                    controller = "AdminGame",
                    action = "RemoveLvl"
                });
        }
    }
}