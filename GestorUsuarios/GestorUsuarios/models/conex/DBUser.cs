using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorUsuarios.models.conex
{
    internal class DBUser
    {
        string stringConex = "server=localhost; user=root; database=managerusers; password=; port=3306;";

        public User GetUser(int id)
        {
            User user = new User();
            string query = "Select * from Users where id = " + id;

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    mySqlConnection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.IdUser = reader.GetInt32("id");
                            user.Nickname = reader.GetString("nickname");
                            user.PasswordHash = reader.GetString("password");
                            user.RolId = reader.GetInt32("idRol");
                            user.PersonId = reader.GetInt32("idPerson");
                            user.Estado = reader.GetBoolean("estado");
                            user.FechaUltimoLogin = reader.GetDateTime("LastLoginDate");
                            user.FechaCreacion = reader.GetDateTime("CreationDate");
                        }
                    }
                }
            }

            return user;
        }

        public Boolean SetUser(User user)
        {

            string queryInsert = "INSERT INTO Users (nickname, password, idRol, idPerson, estado, " +
                                 "LastLoginDate, CreationDate) VALUES (@nicknameUser, @passwordUser, @rolUser, " +
                                 "@personUser, @estadoUser, @lastLoginUser, @creationDateUser)";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryInsert, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@nicknameUser", user.Nickname);
                    command.Parameters.AddWithValue("@passwordUser", user.PasswordHash);
                    command.Parameters.AddWithValue("@rolUser", user.RolId);
                    command.Parameters.AddWithValue("@personUser", user.PersonId);
                    command.Parameters.AddWithValue("@estadoUser", user.Estado);
                    command.Parameters.AddWithValue("@lastLoginUser", user.FechaUltimoLogin);
                    command.Parameters.AddWithValue("@creationDateUser", user.FechaCreacion);

                    mySqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Boolean DeleteUser(int id)
        {
            string queryDelete = "DELETE FROM Users WHERE id = " + id;

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryDelete, mySqlConnection))
                {
                    mySqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean UpdateUser(User user)
        {
            string queryUpdate = "UPDATE Users SET nickname = @nicknameUser, password = @passwordUser, " +
                                 "idRol = @rolUser, idPerson = @personUser, estado = @estadoUser " +
                                 "WHERE id = @idUser";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryUpdate, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@nicknameUser", user.Nickname);
                    command.Parameters.AddWithValue("@passwordUser", user.PasswordHash);
                    command.Parameters.AddWithValue("@rolUser", user.RolId);
                    command.Parameters.AddWithValue("@personUser", user.PersonId);
                    command.Parameters.AddWithValue("@estadoUser", user.Estado);
                    command.Parameters.AddWithValue("@idUser", user.IdUser);

                    mySqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
