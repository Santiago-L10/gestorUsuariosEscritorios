using GestorUsuarios.models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace GestorUsuarios.services
{
    public class UserService : DBConnection
    {
        public List<User> ObtenerUsuarios()
        {
            List<User> lista = new List<User>();

            try
            {
                string query = @"
                SELECT 
                us.id,
                us.idPerson AS idPersona,
                nickname AS nombre,
                password AS passwordHash,
                estado,
                LastLoginDate AS fechaUltimoLogin,
                us.idRol AS rolId,
                CreationDate AS fechaCreacion,
                r.name rol,
                p.email email
                FROM users us, persons p, roles r
                WHERE us.idRol = r.id AND us.idPerson = p.id
                ";

                var cmd = new MySqlCommand(query, Open());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var usuario = new User(
                        reader.GetInt32("id"),
                        reader.GetString("nombre"),
                        reader.GetString("email"),
                        reader.GetString("passwordHash"),
                        30,
                        reader.GetDateTime("fechaCreacion"),
                        reader.GetInt32("rolId")
                    )
                    {
                        Estado = reader.GetBoolean("estado"),
                        FechaUltimoLogin = reader.GetDateTime("fechaUltimoLogin"),
                        Rol = new Rol(reader.GetString("rol")),
                        IdPersona = reader.GetInt32("idPersona")
                    };

                    lista.Add(usuario);
                }

                reader.Close();
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuarios: " + ex.Message);
            }

            return lista;
        }

        public User? ValidarCredenciales(string nickname, string password, out bool usuarioInactivo)
        {
            usuarioInactivo = false;

            try
            {
                string query = @"
                SELECT 
                    us.id,
                    us.nickname AS nombre,
                    us.password AS passwordHash,
                    us.estado,
                    us.LastLoginDate AS fechaUltimoLogin,
                    us.CreationDate AS fechaCreacion,
                    us.idRol AS rolId,
                    r.name AS rol,
                    p.email AS email
                FROM users us
                JOIN persons p ON us.idPerson = p.id
                JOIN roles r ON us.idRol = r.id
                WHERE us.nickname = @nickname AND us.password = @password";

                var cmd = new MySqlCommand(query, Open());
                cmd.Parameters.AddWithValue("@nickname", nickname);
                cmd.Parameters.AddWithValue("@password", password);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.GetBoolean("estado"))
                    {
                        usuarioInactivo = true;
                        reader.Close();
                        Close();
                        return null;
                    }

                    var user = new User(
                        reader.GetInt32("id"),
                        reader.GetString("nombre"),
                        reader.GetString("email"),
                        reader.GetString("passwordHash"),
                        30,
                        reader.GetDateTime("fechaCreacion"),
                        reader.GetInt32("rolId")
                    )
                    {
                        Estado = true,
                        FechaUltimoLogin = reader.GetDateTime("fechaUltimoLogin"),
                        Rol = new Rol(reader.GetString("rol"))
                    };

                    reader.Close();
                    Close();
                    return user;
                }

                reader.Close();
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al validar usuario: " + ex.Message);
            }

            return null;
        }

        public void ActualizarUsuario(User user)
        {
            try
            {
                var conn = Open();

                
                string queryUsers = @"UPDATE users 
                              SET nickname = @nickname, password = @password, estado = @estado, idRol = @rolId 
                              WHERE id = @id";

                var cmdUsers = new MySqlCommand(queryUsers, conn);
                cmdUsers.Parameters.AddWithValue("@nickname", user.Nombre);
                cmdUsers.Parameters.AddWithValue("@password", user.PasswordHash);
                cmdUsers.Parameters.AddWithValue("@estado", user.Estado);
                cmdUsers.Parameters.AddWithValue("@rolId", user.RolId);
                cmdUsers.Parameters.AddWithValue("@id", user.Id);
                cmdUsers.ExecuteNonQuery();

                
                string queryPersons = @"UPDATE persons 
                                SET name = @name, lastname = @lastname, email = @correo 
                                WHERE id = @idPerson";

                var cmdPersons = new MySqlCommand(queryPersons, conn);
                cmdPersons.Parameters.AddWithValue("@name", user.Nombre); 
                cmdPersons.Parameters.AddWithValue("@lastname", user.Apellido);
                cmdPersons.Parameters.AddWithValue("@correo", user.Email);
                cmdPersons.Parameters.AddWithValue("@idPerson", user.IdPersona); 
                cmdPersons.ExecuteNonQuery();

                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar usuario: " + ex.Message);
            }
        }


        public void EliminarUsuario(int idUsuario)
        {
            try
            {
                string query = "DELETE FROM users WHERE id = @id";
                var cmd = new MySqlCommand(query, Open());
                cmd.Parameters.AddWithValue("@id", idUsuario);
                cmd.ExecuteNonQuery();
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar usuario: " + ex.Message);
            }
        }
    }
}
