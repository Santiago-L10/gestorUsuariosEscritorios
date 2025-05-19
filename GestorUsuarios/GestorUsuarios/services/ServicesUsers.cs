using System.Data.SqlTypes;
using System.Windows;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;
using MySql.Data.MySqlClient;

namespace GestorUsuarios.services
{
    internal class ServicesUsers
    {
        public DBUser dbUser;
        public DBPerson dBPerson;
        public ServicesUsers(DBUser userDatabase, DBPerson dBPerson)
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
                    if (user.Estado) {
                        if (user.PasswordHash == password)
                        {
                            SessionUser.Login(user);
                            return true;
                        }
                        MessageBox.Show("Credenciales incorrectas");
                        return false;
                    }
                    MessageBox.Show("Usuario inactivo\nSolicita recuperar credenciales");
                    return false;
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            return false;
        }

        public User GetUser(int? idUser, string nickname = null)
        {
            return dbUser.GetUser(idUser, nickname);
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

        public bool updateUser(User user)
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

        public List<User> listUsers()
        {
            return dbUser.listUsers();
        }

        public void AddUser(User user) { 
            dbUser.SetUser(user);
        }
    }
}
