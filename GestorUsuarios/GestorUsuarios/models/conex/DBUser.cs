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

        public User GetUser(int? id, string nickname = null)
        {
            User user = null;
            string query = "SELECT * FROM Users WHERE id = @id OR nickname = @nickname";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@id", (object)id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@nickname", (object)nickname ?? DBNull.Value);

                    mySqlConnection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Solo obtener el primer resultado
                        {
                            user = new User
                            {
                                IdUser = reader.GetInt32("id"),
                                Nickname = reader.GetString("nickname"),
                                PasswordHash = reader.GetString("password"),
                                RolId = reader.GetInt32("idRol"),
                                PersonId = reader.GetInt32("idPerson"),
                                Estado = reader.GetBoolean("estado"),
                                FechaUltimoLogin = reader.GetDateTime("LastLoginDate"),
                                FechaCreacion = reader.GetDateTime("CreationDate")
                            };
                        }
                    }
                }
            }

            return user;
        }


        public bool SetUser(User user)
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

        public bool DeleteUser(int id)
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

        public bool UpdateUser(User user)
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
