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

                    command.Parameters.AddWithValue("@login", people.UserLogin.ToLower());
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

                    cmd.Parameters.AddWithValue("@login", Login.ToLower());

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

                    cmd.Parameters.AddWithValue("@login", oldPeople.UserLogin.ToLower());
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
                    catch (Exception e)
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
        /// Получить полную информацию информацию об игре
        /// </summary>
        /// <param name="gameId">ID игры</param>
        /// <returns></returns>
        public static Game GetGame(int gameId)
        {
            Game game = new Game();

            // Тут запрос в БД!
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                                      "FROM [Game] " +
                                      "WHERE Id = @gameId";

                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //result.Add(Convert.ToInt32(reader["playerId"]));

                            game.Id = (int)reader["Id"];
                            game.NameGame = reader["nameGame"].ToString();
                            game.IdАuthor = (int)reader["authorId"];
                            game.Sequence = reader["sequence"].ToString();
                            game.Distance = (int)reader["distance"];
                            game.StartGame = (DateTime)reader["startGame"];
                            game.EndGame = (DateTime)reader["endGame"];
                            game.Info = reader["info"].ToString();
                            game.AmountLevels = (int)reader["amountLevels"];
                        }
                    }
                }
            }

            return game;

            //game.Id = gameId;
            //game.NameGame = "Первая игра \"движка\"";
            //game.IdАuthor = new[] {1, 5};
            //game.Sequence = "Линейная";
            //game.Distance = 50;
            //game.AmountLevels = 10;

            //DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1, 22, 35, 00);
            //game.StartGame = dateTime;

            //// TODO Не забывать, что переносы в THML не работают! весь текст нужно как-то конвертировать в формат HTML
            //game.Info = "появившаяся в 2005 году российская командная игра в формате ночных поисковых игр, включающая в себя " +
            //            "соревнование по городскому ориентированию, актерские и ролевые уровни, экстремальные и логические " +
            //            "задания. В основе лежит американский паззлхант и его вариации (Форт Боярд, Fear Factor, и др.) \n" +
            //            "Игра состоит из 10 основных заданий, в каждом из которых зашифровано местоположение локации " +
            //            "(заброса), где находятся коды или человек в городе. " +
            //            "Задача — пройти последовательно все 10 уровней раньше других команд, выполнить все дополнительные задания";

            //return game;
        }

        /// <summary>
        /// Получение игрового задания
        /// </summary>
        /// <param name="gameId">ID игры</param>
        /// <param name="NumberLevel">Порядковый номер задания</param>
        /// <returns></returns>
        public static Quest GetQuest(int gameId, int NumberLevel)
        {
            Quest quest = new Quest();

            #region БД

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                                      "FROM [Quest] " +
                                      "WHERE numberLevel = @numberLevel and gameId = @gameId";

                    cmd.Parameters.AddWithValue("@numberLevel", NumberLevel);
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        int result = 0;

                        if (reader.Read())
                        {
                            //result = int.Parse((string) reader["numberLevel"]);

                            quest.Id = (int)reader["Id"];
                            quest.GameId = (int)reader["gameId"];
                            quest.NumberLevel = (int)reader["numberLevel"];
                            quest.AuthorComment = (string)reader["authorComment"];
                            quest.TimeOut = (int)reader["timeout"];
                            quest.TextQuest = (string)reader["textQuest"];
                            quest.NameLevel = (string)reader["nameLevel"];
                        }
                        return quest;
                    }
                }
            }

            #endregion

            //quest.Id = 1;
            //quest.AuthorComment = "Код синим маркером<br>Подробности по телефону 8-909-076-75-06";
            //quest.NumberLevel = NumberLevel;
            //quest.TextQuest = "\"...Мы с сестрой с детства мечтали о своём бизнесе. Эми помогла нам осуществить нашу мечту. И вот несколько месяцев назад мы открылись.Удалось ухватить площадь на месте бывшего гальванического цеха...\" <br /> <br />  Примечание: спросить мою сестру Марго.";
            //quest.TimeOut = 40;

            return quest;
        }

        /// <summary>
        /// Получить список команд, которые играют в эту игру
        /// </summary>
        /// <param name="gameId">ID игры</param>
        /// <returns></returns>
        public static List<int> GetGameTeamId(int gameId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT [GamingTeam].[teamId] " +
                                      "FROM [GamingTeam] " +
                                      "WHERE access = @access and gameId = @gameId";

                    cmd.Parameters.AddWithValue("@access", 1);
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var result = new List<int>();

                        while (reader.Read())
                        {
                            result.Add(Convert.ToInt32(reader["teamId"]));
                        }

                        return result;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Получить время начала следующего уровня и номер уровня, на котором сейчас находится команда
        /// </summary>
        /// <param name="teamId">ID команды</param>
        /// <param name="gameId">ID игры</param>
        /// <returns></returns>
        public static LvlAndTime GetPositionInGame(int teamId, int gameId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT [numberLevel], [answerTime] " +
                                      "FROM [PositionInTheGame] " +
                                      "WHERE teamId = @teamId and gameId = @gameId";

                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<LvlAndTime> foo = new List<LvlAndTime>();

                        while (reader.Read())
                        {
                            //foo.Add((int) reader["numberLevel"]);
                            LvlAndTime buff = new LvlAndTime();
                            buff.numburLVL = 1 + (int)reader["numberLevel"];
                            buff.StartLVL = (DateTime)reader["answerTime"];
                            foo.Add(buff);
                        }

                        if (foo.Count == 0)
                        {
                            return null;
                        }
                        else
                        {
                            int max = 0;
                            int maxi = 0;

                            // Поиск максимального уровня
                            for (int i = 0; i < foo.Count; i++)
                            {
                                if (foo[i].numburLVL >= max)
                                {
                                    max = foo[i].numburLVL;
                                    maxi = i;
                                }
                            }

                            return foo[maxi];
                            // TODO Найти максимальный уровень и вернуть это значение!
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Проверить существует ли такой ответ на задание
        /// </summary>
        /// <param name="questId">ID задания</param>
        /// <param name="answer">Ответ</param>
        /// <returns></returns>
        public static bool AnswerCheck(int questId, string answer)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT [id] " +
                                      "FROM [Answers] " +
                                      "WHERE questId = @questId and answer = @answer";

                    cmd.Parameters.AddWithValue("@questId", questId);
                    cmd.Parameters.AddWithValue("@answer", answer);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Полный список автопереходов одной игры
        /// </summary>
        /// <param name="gameId">ID игры</param>
        /// <returns></returns>
        public static List<int> GetTimeoutAllQuest(int gameId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT [timeout] " +
                                      "FROM [Quest] " +
                                      "WHERE gameId = @gameId";

                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var result = new List<int>();

                        while (reader.Read())
                        {
                            result.Add(Convert.ToInt32(reader["timeout"]));
                        }

                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// Задать текущий уровень в игре для команды и сделать пометку того, кто сделал правильный ответ и время ответа
        /// </summary>
        /// <param name="gameId">ID игры</param>
        /// <param name="numberLevel">Номер игрового уровня</param>
        /// <param name="teamId">ID команды</param>
        /// <param name="playerId">ID игрока, сделавший правильный ответ. ПО УМОЛЧАНИЮ 0 ЕСЛИ ПО АВТОПЕРЕХОДУ</param>
        /// <returns></returns>
        public static bool SetCurrentLevelForTeam(int gameId, int numberLevel, int teamId, int playerId = 0)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO [PositionInTheGame] (gameId, numberLevel, teamId, playerId, answerTime) " +
                        " VALUES (@gameId, @numberLevel, @teamId, @playerId, @answerTime);";

                    cmd.Parameters.AddWithValue("@gameId", gameId);
                    cmd.Parameters.AddWithValue("@numberLevel", numberLevel);
                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    cmd.Parameters.AddWithValue("@playerId", playerId);
                    cmd.Parameters.AddWithValue("@answerTime", DateTime.Now);

                    //command.ExecuteNonQueryAsync();     // асинхронно
                    try
                    {
                        cmd.ExecuteNonQuery();     // синхронно
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Получить список предстоящих игр!
        /// </summary>
        /// <returns></returns>
        public static List<Game> GetListGames()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                                      "FROM [Game] " +
                                      "WHERE endGame > @datetime;";

                    cmd.Parameters.AddWithValue("@datetime", DateTime.Now);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var result = new List<Game>();

                        while (reader.Read())
                        {
                            Game game = new Game();

                            game.Id = (int)reader["Id"];
                            game.NameGame = (string)reader["nameGame"];
                            game.AmountLevels = (int)reader["authorId"];
                            game.Sequence = (string)reader["sequence"];
                            game.Distance = (int)reader["distance"];
                            game.StartGame = (DateTime)reader["startGame"];
                            game.EndGame = (DateTime)reader["endGame"];
                            game.Info = (string)reader["info"];
                            game.AmountLevels = (int)reader["amountLevels"];

                            result.Add(game);
                        }

                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// Добавить игрока в команду
        /// </summary>
        /// <param name="peopleId">Id Игрока</param>
        /// <param name="teamId">Id Команды</param>
        /// <returns></returns>
        public static bool AddPlayerTeam(int peopleId, int teamId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [playerTeam] (playerId, teamId)" +
                        " VALUES(@playerId, @teamId);";

                    cmd.Parameters.AddWithValue("@playerId", peopleId);
                    cmd.Parameters.AddWithValue("@teamId", teamId);

                    try
                    {
                        cmd.ExecuteNonQuery();
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
        /// Создать команду
        /// </summary>
        /// <param name="teamName">Название команды</param>
        /// <param name="teamComanderId">Id Командира</param>
        /// <returns></returns>
        public static int CreateTeam(string teamName, int teamComanderId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [team] (teamName, teamComanderId) " +
                        "VALUES(@teamName, @teamComanderId) " +
                        "SELECT SCOPE_IDENTITY() as 'Id'";

                    cmd.Parameters.AddWithValue("@teamName", teamName);
                    cmd.Parameters.AddWithValue("@teamComanderId", teamComanderId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        int teamId = -1;

                        if (reader.Read())
                        {
                            teamId = Convert.ToInt32(reader["Id"]);
                            return teamId;
                        }
                        return teamId;
                    }
                }
            }
        }

        /// <summary>
        /// Получить список игр по id автора
        /// </summary>
        /// <returns></returns>
        public static List<Game> GetListGameForPeopleId(int idAuthor)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT Game.* " +
                                      "FROM ND.dbo.users INNER JOIN (ND.dbo.Game INNER JOIN ND.dbo.AuthorGame ON Game.Id = AuthorGame.idGame) ON users.Id = AuthorGame.idAuthor " +
                                      "WHERE users.Id = @idAuthor;";

                    cmd.Parameters.AddWithValue("@idAuthor", idAuthor);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var result = new List<Game>();

                        while (reader.Read())
                        {
                            Game game = new Game();

                            game.Id = (int)reader["Id"];
                            game.NameGame = (string)reader["nameGame"];
                            game.AmountLevels = (int)reader["authorId"];
                            game.Sequence = (string)reader["sequence"];
                            game.Distance = (int)reader["distance"];
                            game.StartGame = (DateTime)reader["startGame"];
                            game.EndGame = (DateTime)reader["endGame"];
                            game.Info = (string)reader["info"];
                            game.AmountLevels = (int)reader["amountLevels"];

                            result.Add(game);
                        }

                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// Обновить информацию об игре
        ///</summary>
        public static bool UpdateInfoGame(Game oldGame, Game updateGame)
        {
            // Не должно меняться
            // oldGame.Id;

            // TODO Доделать изменения
            //oldGame.AmountLevels;


            oldGame.Distance = updateGame.Distance;

            if (!string.IsNullOrEmpty(updateGame.Info))
            {
                oldGame.Info = updateGame.Info;
            }

            if (!string.IsNullOrEmpty(updateGame.NameGame))
            {
                oldGame.NameGame = updateGame.NameGame;
            }

            if (!string.IsNullOrEmpty(updateGame.Sequence))
            {
                oldGame.Sequence = updateGame.Sequence;
            }

            // TODO Для начала сделать передачу времени на клиента, а уже потом изменения 
            oldGame.StartGame = updateGame.StartGame;
            oldGame.EndGame = updateGame.EndGame;
            //oldGame.StartGame;
            //oldGame.EndGame;

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "UPDATE [Game] " +
                                      "SET " +
                                            "nameGame = @nameGame, " +
                                            "sequence = @sequence, " +
                                            "distance = @distance, " +
                                            "startGame = @startGame, " +
                                            "endGame = @endGame, " +
                                            "info = @info, " +
                                            "amountLevels = @amountLevels " +
                                      "WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("@nameGame", oldGame.NameGame);
                    cmd.Parameters.AddWithValue("@sequence", oldGame.Sequence);
                    cmd.Parameters.AddWithValue("@distance", oldGame.Distance);
                    cmd.Parameters.AddWithValue("@startGame", oldGame.StartGame);
                    cmd.Parameters.AddWithValue("@endGame", oldGame.EndGame);
                    cmd.Parameters.AddWithValue("@info", oldGame.Info);
                    cmd.Parameters.AddWithValue("@amountLevels", oldGame.AmountLevels);
                    cmd.Parameters.AddWithValue("@Id", oldGame.Id);


                    try
                    {
                        cmd.ExecuteNonQuery();     // синхронно
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                        // TODO сделать лог
                    }

                }
            }
        }

        /// <summary>
        /// Обновить ответ на задание
        /// </summary>
        /// <param name="quest">Модель уровня</param>
        /// <returns></returns>
        public static bool UpdateQuest(Quest quest)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE [Quest] " +
                                      "SET " +
                                      "nameLevel = @nameLevel," +
                                      "authorComment = @authorComment," +
                                      "timeout = @timeout, " +
                                      "textQuest = @textQuest " +
                                      "WHERE " +
                                      "gameId = @gameId and numberLevel = @numberLevel";

                    cmd.Parameters.AddWithValue("@nameLevel", quest.NameLevel ?? "");
                    cmd.Parameters.AddWithValue("@authorComment", quest.AuthorComment ?? "");
                    cmd.Parameters.AddWithValue("@timeout", quest.TimeOut);
                    cmd.Parameters.AddWithValue("@textQuest", quest.TextQuest ?? "");
                    cmd.Parameters.AddWithValue("@gameId", quest.GameId);
                    cmd.Parameters.AddWithValue("@numberLevel", quest.NumberLevel);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                        // TODO сделать лог
                    }
                }
            }
        }

        /// <summary>
        /// Получить список ответов на уровень
        /// </summary>
        /// <returns></returns>
        public static List<Answer> GetListAnswersOnLvl(int questId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                                      "FROM Answers " +
                                      "WHERE questId = @questId;";

                    cmd.Parameters.AddWithValue("@questId", questId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var result = new List<Answer>();

                        while (reader.Read())
                        {
                            Answer game = new Answer();

                            game.Id = (int)reader["Id"];
                            game.TextAnswer = (string)reader["answer"];
                            game.QuestId = (int)reader["questId"];

                            result.Add(game);
                        }

                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// Создать ответ на задание
        /// </summary>
        /// <param name="questId">id задания</param>
        /// <param name="answer">текст ответа</param>
        /// <returns></returns>
        public static bool CreateAnswers(int questId, string answer)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [Answers] (questId, answer) " +
                                      "VALUES(@questId, @answer) ";

                    cmd.Parameters.AddWithValue("@questId", questId);
                    cmd.Parameters.AddWithValue("@answer", answer);

                    try
                    {
                        cmd.ExecuteNonQuery();
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
        /// Обновить ответ на задание
        /// </summary>
        /// <param name="answerId">id ответа</param>
        /// <param name="answer">Текст ответа</param>
        /// <returns></returns>
        public static bool UpdateAnswers(int answerId, string answer)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE [Answers] " +
                                      "SET answer = @answer " +
                                      "WHERE Id = @answerId";

                    cmd.Parameters.AddWithValue("@answer", answer);
                    cmd.Parameters.AddWithValue("@answerId", answerId);

                    try
                    {
                        cmd.ExecuteNonQuery();
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
        /// Удалить ответ на задание
        /// </summary>
        /// <param name="answerId">Id ответа</param>
        /// <returns></returns>
        public static bool RemoveAnswers(int answerId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE " +
                                      "FROM [Answers] " +
                                      "WHERE Id = @answerId";

                    cmd.Parameters.AddWithValue("@answerId", answerId);

                    try
                    {
                        cmd.ExecuteNonQuery();
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
        /// Получить список уровней в задании
        /// </summary>
        /// <param name="gameId">Id игры</param>
        /// <returns></returns>
        public static List<Quest> GetListQuest(int gameId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                                      "FROM [Quest] " +
                                      "WHERE gameId = @gameId " +
                                      "ORDER BY numberLevel; ";

                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var result = new List<Quest>();

                        while (reader.Read())
                        {
                            Quest quest = new Quest();

                            quest.Id = (int)reader["Id"];
                            quest.GameId = (int)reader["gameId"];
                            quest.NumberLevel = (int)reader["numberLevel"];
                            quest.NameLevel = (string)reader["nameLevel"];
                            quest.AuthorComment = (string)reader["authorComment"];
                            quest.TimeOut = (int)reader["timeout"];
                            quest.TextQuest = (string)reader["textQuest"];

                            result.Add(quest);
                        }

                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// Получить команд подавших заявки
        /// </summary>
        /// <param name="gameId">Id игры</param>
        /// <returns></returns>
        public static List<TeamPlay> GetListTeamPlay(int gameId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                                      "FROM [GamingTeam] " +
                                      "WHERE gameId = @gameId;";

                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var result = new List<TeamPlay>();

                        while (reader.Read())
                        {
                            TeamPlay quest = new TeamPlay();

                            quest.Id = (int)reader["Id"];
                            quest.GameId = (int)reader["gameId"];
                            quest.TeamId = (int)reader["teamId"];
                            quest.Access = (bool)reader["access"];

                            result.Add(quest);
                        }

                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// Изменить запись по заявке на игру
        /// </summary>
        /// <returns></returns>
        public static bool UpdateTeamPlay(int id, bool access)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE [GamingTeam] " +
                                      "SET access = @access " +
                                      "WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@access", access);
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        cmd.ExecuteNonQuery();
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
        /// Удалить заявку о участии в игре
        /// </summary>
        /// <param name="gameId">Id игры</param>
        /// <param name="teamId">Id команды</param>
        /// <returns></returns>
        public static bool RemoveApplication(int gameId, int teamId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE " +
                                      "FROM [GamingTeam] " +
                                      "WHERE gameId = @gameId and teamId = @teamId";

                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    try
                    {
                        cmd.ExecuteNonQuery();
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
        /// Записать заявку на участие в игре
        /// </summary>
        /// <param name="gameId">id игры</param>
        /// <param name="teamId">id команды</param>
        /// <returns></returns>
        public static bool AddApplication(int gameId, int teamId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [GamingTeam] (gameId, teamId, access) " +
                                      "VALUES(@gameId, @teamId, @access) ";

                    cmd.Parameters.AddWithValue("@gameId", gameId);
                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    cmd.Parameters.AddWithValue("@access", false);

                    try
                    {
                        cmd.ExecuteNonQuery();
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

        public static int CreateGame(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [Game] (nameGame, authorId, sequence, distance, startGame, endGame, info, amountLevels ) " +
                                      "VALUES(@nameGame, @authorId, @sequence, @distance, @startGame, @endGame, @info, @amountLevels) " +
                                      "SELECT SCOPE_IDENTITY() as 'Id'";

                    cmd.Parameters.AddWithValue("@nameGame", "New Game");
                    cmd.Parameters.AddWithValue("@authorId", id);
                    cmd.Parameters.AddWithValue("@sequence", "Линейная");
                    cmd.Parameters.AddWithValue("@distance", 50);
                    cmd.Parameters.AddWithValue("@startGame", new DateTime(2000, 1, 1, 20, 00, 00));
                    cmd.Parameters.AddWithValue("@endGame", new DateTime(2000, 1, 1, 20, 00, 00));
                    cmd.Parameters.AddWithValue("@info", "");
                    cmd.Parameters.AddWithValue("@amountLevels", 1);

                    using (var reader = cmd.ExecuteReader())
                    {
                        int gameId = -1;

                        if (reader.Read())
                        {
                            gameId = Convert.ToInt32(reader["Id"]);
                            return gameId;
                        }
                        return gameId;
                    }
                }
            }
        }

        public static bool AddAuthorGame(int idAuthor, int idGame)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [AuthorGame] (idAuthor, idGame) " +
                                      "VALUES(@idAuthor, @idGame) ";

                    cmd.Parameters.AddWithValue("@idAuthor", idAuthor);
                    cmd.Parameters.AddWithValue("@idGame", idGame);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                        // TODO сделать лог
                    }
                }
            }
        }
    }
}


//DELETE FROM ND.dbo.users
//WHERE Id = 2