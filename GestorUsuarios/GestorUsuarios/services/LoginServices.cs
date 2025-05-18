using System.Data.SqlTypes;
using System.Windows;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;
using MySql.Data.MySqlClient;

namespace GestorUsuarios.services
{
    internal class LoginServices
    {
        public DBUser dbUser;
        public DBPerson dBPerson;
        public LoginServices(DBUser userDatabase, DBPerson dBPerson)
        {
            this.dbUser = userDatabase;
            this.dBPerson = dBPerson;
        }

        public bool Authentication(string nickname, string password)
        {
            try
            {
                User user = dbUser.GetUser(null, nickname);
                if (user != null)
                {
                    if (user.PasswordHash == password)
                    {
                        SessionUser.Login(user);
                        return true;
                    }
                    MessageBox.Show("Credenciales incorrectas");
                    return false;
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            return false;
        }

        public Person dataUSer(int idPerson)
        {
            return dBPerson.GetPerson(idPerson);
        }

        public bool updateUser(Person person)
        {
            try
            {
                return dBPerson.UpdatePerson(person);
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool updateUserPassword(User user)
        {
            try
            {
                return dbUser.UpdateUser(user);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
