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
    }
}


//DELETE FROM ND.dbo.users
//WHERE Id = 2