using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class DatabaseND
    {
        private readonly static string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ND;Integrated Security=SSPI;";

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

        public static People SearchPeople(string Login)
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

    }
}


//DELETE FROM ND.dbo.users
//WHERE Id = 2