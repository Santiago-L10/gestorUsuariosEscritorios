using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorUsuarios.models.conex
{
    internal class DBRol
    {
        string stringConex = "server=localhost; user=root; database=managerusers; password=; port=3306;";

        public Rol GetRol(int id)
        {
            Rol rol = new Rol();
            string query = "Select * from Roles where id = " + id;

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    mySqlConnection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rol.IdRol = reader.GetInt32("id");
                            rol.NameRol = reader.GetString("name");
                        }
                    }
                }
            }

            return rol;
        }

        public List<Rol> GetListRols()
        {
            List<Rol> listRols = new List<Rol>();
            string query = "Select * from roles";
            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    mySqlConnection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rol rol = new Rol();
                            rol.IdRol = reader.GetInt32("id");
                            rol.NameRol = reader.GetString("name");
                            listRols.Add(rol);
                        }
                    }
                }
            }
            return listRols;
        }

        public bool SetRol(Rol rol)
        {

            string queryInsert = "INSERT INTO Roles (name) VALUES (@nameRol)";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryInsert, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@nameRol", rol.NameRol);

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

        public bool DeleteRol(int id)
        {
            string queryDelete = "DELETE FROM Roles WHERE id = " + id;

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

        public bool UpdateRol(Rol rol)
        {
            string queryUpdate = "UPDATE Roles SET name = @nameRol WHERE id = @idRol";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryUpdate, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@nameRol", rol.NameRol);
                    command.Parameters.AddWithValue("@idRol", rol.IdRol);

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
