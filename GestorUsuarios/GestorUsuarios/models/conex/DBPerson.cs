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
        string stringConex = "server=localhost; user=root; database=gestorUsuarios; password=; port=3306;";

        public Person GetPerson(int id)
        {
            Person person = new Person();
            string query = "Select * from Persons where idPerson = " + id;

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    mySqlConnection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            person.IdPerson = reader.GetInt32("idPerson");
                            person.NamesPerson = reader.GetString("namesPerson");
                            person.LastNamesPerson = reader.GetString("lastNamesPerson");
                            person.AgePerson = reader.GetInt32("agePerson");
                            person.EmailPerson = reader.GetString("emailPerson");
                        }
                    }
                }
            }

            return person;
        }

        public Boolean SetPerson(Person person)
        {

            string queryInsert = "INSERT INTO Persons (namesPerson, lastNamesPerson, agePerson, emailPerson) VALUES (@names, @lastNames, @age, @email)";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryInsert, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@names", person.NamesPerson);
                    command.Parameters.AddWithValue("@lastnames", person.LastNamesPerson);
                    command.Parameters.AddWithValue("@age", person.AgePerson);
                    command.Parameters.AddWithValue("@email", person.EmailPerson);

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
            string queryDelete = "DELETE FROM Persons WHERE idPerson = " + id;

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
            string queryUpdate = "UPDATE Persons SET namesPerson = @names, lastNamesPerson = @lastNames, agePerson = @age, emailPerson = @email WHERE idPerson = @id";

            using (MySqlConnection mySqlConnection = new MySqlConnection(stringConex))
            {
                using (MySqlCommand command = new MySqlCommand(queryUpdate, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@names", person.NamesPerson);
                    command.Parameters.AddWithValue("@lastNames", person.LastNamesPerson);
                    command.Parameters.AddWithValue("@age", person.AgePerson);
                    command.Parameters.AddWithValue("@email", person.EmailPerson);
                    command.Parameters.AddWithValue("@id", person.IdPerson);

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
