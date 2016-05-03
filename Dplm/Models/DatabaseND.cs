using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    /// <summary>
    /// Работа с базой данных
    ///</summary>
    public class DatabaseND
    {
        private readonly static string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ND;Integrated Security=SSPI;";

        /// <summary>
        /// Добавить в базу нового пользователя
        ///</summary>
        public static bool AddUser(People people)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    //command.CommandText = "INSERT INTO [users] (userLogin, userPass, phoneNumber, email, lastName, firstName, birthday, linkVK)" +
                    //    " VALUES(@login, @pass, @phone, @email, @lastName, @firstName, @birth, @vk);";
                    command.CommandText = "INSERT INTO [users] (userLogin, userPass, phoneNumber, email)" +
                        " VALUES(@login, @pass, @phone, @email);";

                    command.Parameters.AddWithValue("@login", people.UserLogin);
                    command.Parameters.AddWithValue("@pass", people.UserPass);
                    command.Parameters.AddWithValue("@phone", people.PhoneNumber);
                    command.Parameters.AddWithValue("@email", people.Email);
                    //command.Parameters.AddWithValue("@lastName", people.FamiluName);
                    //command.Parameters.AddWithValue("@firstName", people.Name);
                    //command.Parameters.AddWithValue("@birth", people.Birthday);
                    //command.Parameters.AddWithValue("@vk", people.LinkVK);

                    //command.ExecuteNonQueryAsync();     // асинхронно
                    try
                    {
                        command.ExecuteNonQuery();     // синхронно
                        return true;
                    }
                    catch
                    {
                        return false;
                        // TODO сделать лог
                    }
                }
            }
        }

        /// <summary>
        ///Найти человека по его логину
        ///</summary>
        public static People SearchPeopleByLogin(string Login)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " + 
                        "FROM [users] " + 
                        " WHERE userLogin = @login";

                    cmd.Parameters.AddWithValue("@login", Login);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            People people = new People();

                            int id;

                            try
                            {
                                id = Convert.ToInt32(reader["Id"]);
                            }
                            catch (Exception)
                            {
                                id = 0;
                            }

                            people.Id = id;
                            people.UserLogin = reader["userLogin"].ToString().Trim();
                            people.UserPass = reader["userPass"].ToString().Trim();
                            people.PhoneNumber = reader["phoneNumber"].ToString().Trim();
                            people.Email = reader["email"].ToString().Trim();
                            people.Name = reader["lastName"].ToString().Trim();
                            people.FamiluName = reader["firstName"].ToString().Trim();
                            people.Birthday = reader["birthday"].ToString().Trim();
                            people.LinkVK = reader["linkVK"].ToString().Trim();

                            return people;
                        }
                    }
                }
            }
                return null; 
        }

        /// <summary>
        /// Найти человека по его ID
        ///</summary>
        public static People SearchPeopleById(int Id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                        "FROM [users] " +
                        " WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", Id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            People people = new People();

                            int id;

                            try
                            {
                                id = Convert.ToInt32(reader["Id"]);
                            }
                            catch (Exception)
                            {
                                id = 0;
                            }

                            people.Id = id;
                            people.UserLogin = reader["userLogin"].ToString().Trim();
                            people.UserPass = reader["userPass"].ToString().Trim();
                            people.PhoneNumber = reader["phoneNumber"].ToString().Trim();
                            people.Email = reader["email"].ToString().Trim();
                            people.Name = reader["lastName"].ToString().Trim();
                            people.FamiluName = reader["firstName"].ToString().Trim();
                            people.Birthday = reader["birthday"].ToString().Trim();
                            people.LinkVK = reader["linkVK"].ToString().Trim();

                            return people;
                        }
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// Обновить данные о игроке
        ///</summary>
        public static bool UpdateUser(People oldPeople, People updatePeople)
        {
            if (!string.IsNullOrEmpty(updatePeople.UserLogin))
            {
                oldPeople.UserLogin = updatePeople.UserLogin;
            }

            // TODO нужно проверять вводилось ли что-то в поля для замены пароля и проверять введеный пароль для изменения данных
            //if (!string.IsNullOrEmpty(updatePeople.UserPass))
            //{
            //    oldPeople.UserPass = updatePeople.UserPass;
            //}

            if (!string.IsNullOrEmpty(updatePeople.PhoneNumber))
            {
                oldPeople.PhoneNumber = updatePeople.PhoneNumber;
            }

            if (!string.IsNullOrEmpty(updatePeople.Email))
            {
                oldPeople.Email = updatePeople.Email;
            }

            if (!string.IsNullOrEmpty(updatePeople.FamiluName))
            {
                oldPeople.FamiluName = updatePeople.FamiluName;
            }

            if (!string.IsNullOrEmpty(updatePeople.Name))
            {
                oldPeople.Name = updatePeople.Name;
            }

            if (!string.IsNullOrEmpty(updatePeople.Birthday))
            {
                oldPeople.Birthday = updatePeople.Birthday;
            }

            if (!string.IsNullOrEmpty(updatePeople.LinkVK))
            {
                oldPeople.LinkVK = updatePeople.LinkVK;
            }


            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "UPDATE [users] " +
                                      "SET " +
                                            "userLogin = @login, " +
                                            "userPass = @pass, " +
                                            "phoneNumber = @phone, " +
                                            "email = @email, " +
                                            "lastName = @lastName, " +
                                            "firstName = @firstName, " +
                                            "linkVK = @vk, " +
                                            "birthday = @birth " +
                                      "WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("@login", oldPeople.UserLogin);
                    cmd.Parameters.AddWithValue("@pass", oldPeople.UserPass);
                    cmd.Parameters.AddWithValue("@phone", oldPeople.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", oldPeople.Email);
                    cmd.Parameters.AddWithValue("@lastName", oldPeople.FamiluName);
                    cmd.Parameters.AddWithValue("@firstName", oldPeople.Name);
                    cmd.Parameters.AddWithValue("@birth", oldPeople.Birthday);
                    cmd.Parameters.AddWithValue("@vk", oldPeople.LinkVK);
                    cmd.Parameters.AddWithValue("@Id", oldPeople.Id);
                    

                    try
                    {
                        cmd.ExecuteNonQuery();     // синхронно
                        return true;
                    }
                    catch(Exception e)
                    {
                        return false;
                        // TODO сделать лог
                    }

                }
            }
        }
        
        /// <summary>
        /// Получить соотношение игрока к команде. -1 если нигде не состоит
        ///</summary>
        public static int ComplianceTeamPlayer(int idPeople)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT [playerTeam].[teamId] " +
                                      "FROM [playerTeam] " +
                                      "WHERE [playerId] = @idPeople";

                    cmd.Parameters.AddWithValue("@idPeople", idPeople);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int teamId;

                            try
                            {
                                teamId = Convert.ToInt32(reader["teamId"]);
                            }
                            catch (Exception)
                            {
                                teamId = -1;
                            }

                            return teamId;
                        }
                    }
                }
            }
            return -1;    // На случай если что-то пойдет не так
        }

        /// <summary>
        /// Получить данные о команде
        ///</summary>
        public static Team GetTeam(int teamId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                                      "FROM [team] " +
                                      "WHERE [Id] = @teamId";

                    cmd.Parameters.AddWithValue("@teamId", teamId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Team team = new Team();

                            try
                            {
                                team.Id = Convert.ToInt32(reader["Id"]);
                                team.IdCommander = Convert.ToInt32(reader["teamComanderId"]);
                            }
                            catch (Exception)
                            {
                                team.Id = 0;
                                team.IdCommander = 0;
                                team.Name = "Ошибка в базе данных!";
                            }

                            team.Name = reader["teamName"].ToString().Trim();

                            return team;
                        }
                    }
                }
            }
            return null;    // На случай если что-то пойдет не так
        }

        /// <summary>
        /// Получить список ID игроков в команде
        /// </summary>
        /// <param name="teamId">Id команды, по которой будет поиск</param>
        /// <returns></returns>
        public static List<int> GetArrayPlayer(int teamId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT [playerTeam].[playerId] " +
                                      "FROM [playerTeam] " +
                                      "WHERE teamId = @teamId";

                    cmd.Parameters.AddWithValue("@teamId", teamId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var result = new List<int>();

                        while (reader.Read())
                        {
                            result.Add(Convert.ToInt32(reader["playerId"]));
                        }

                        return result;
                    }
                }
            }

            return null;    // На случай если что-то пойдет не так
        }

        /// <summary>
        /// Заглушка метода получения игры из БД
        /// </summary>
        /// <param name="gameId">ID игры</param>
        /// <returns></returns>
        public static Game GetGame(int gameId)
        {
            Game game = new Game();

            #region

            // Тут запрос в БД!

            #endregion

            game.Id = gameId;
            game.NameGame = "Первая игра \"движка\"";
            game.IdАuthor = new[] {1, 5};
            game.Sequence = "Линейная";
            game.Distance = 50;

            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1, 22, 35, 00);
            game.StartGame = dateTime;

            // TODO Не забывать, что переносы в THML не работают! весь текст нужно как-то конвертировать в формат HTML
            game.Info = "появившаяся в 2005 году российская командная игра в формате ночных поисковых игр, включающая в себя " +
                        "соревнование по городскому ориентированию, актерские и ролевые уровни, экстремальные и логические " +
                        "задания. В основе лежит американский паззлхант и его вариации (Форт Боярд, Fear Factor, и др.) \n" +
                        "Игра состоит из 10 основных заданий, в каждом из которых зашифровано местоположение локации " +
                        "(заброса), где находятся коды или человек в городе. " +
                        "Задача — пройти последовательно все 10 уровней раньше других команд, выполнить все дополнительные задания";

            return game;
        }
    }
}


//DELETE FROM ND.dbo.users
//WHERE Id = 2