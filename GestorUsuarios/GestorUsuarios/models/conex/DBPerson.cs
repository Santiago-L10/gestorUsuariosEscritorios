using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorUsuarios.models.conex
{
    internal class DBPerson
    {
        string stringConex = "server=localhost; user=root; database=managerusers; password=; port=3306;";

        public Person GetPerson(int id)
        {
            Person person = new Person();
            string query = "Select * from Persons where id = " + id;

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    mySqlConnection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            person.IdPerson = reader.GetInt32("id");
                            person.NamesPerson = reader.GetString("name");
                            person.LastNamesPerson = reader.GetString("lastname");
                            person.AgePerson = reader.GetInt32("age");
                            person.EmailPerson = reader.GetString("email");
                        }
                    }
                }
            }

            return person;
        }

        public Boolean SetPerson(Person person)
        {

            string queryInsert = "INSERT INTO Persons (name, lastname, age, email) VALUES (@namesPerson, @lastNamesPerson, @agePerson, @emailPerson)";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryInsert, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@namesPerson", person.NamesPerson);
                    command.Parameters.AddWithValue("@lastNamesPerson", person.LastNamesPerson);
                    command.Parameters.AddWithValue("@agePerson", person.AgePerson);
                    command.Parameters.AddWithValue("@emailPerson", person.EmailPerson);

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

        public Boolean DeletePerson(int id)
        {
            string queryDelete = "DELETE FROM Persons WHERE id = " + id;

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

        public Boolean UpdatePerson(Person person)
        {
            string queryUpdate = "UPDATE Persons SET name = @namesPerson, lastname = @lastNamesPerson, age = @agePerson, email = @emailPerson WHERE id = @idPerson";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryUpdate, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@namesPerson", person.NamesPerson);
                    command.Parameters.AddWithValue("@lastNamesPerson", person.LastNamesPerson);
                    command.Parameters.AddWithValue("@agePerson", person.AgePerson);
                    command.Parameters.AddWithValue("@emailPerson", person.EmailPerson);
                    command.Parameters.AddWithValue("@idPerson", person.IdPerson);

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
