using System.Windows;
using GestorUsuarios.models;
using GestorUsuarios.services;
using GestorUsuarios.views;

namespace GestorUsuarios.controller
{
    internal class ControllerUser
    {
        public ServicesUsers logServices;
        public ControllerUser(ServicesUsers loginServices)
        {
            logServices = loginServices;
        }
        public bool acces(string nickname, string password)
        {
            bool r = logServices.Authentication(nickname, password);
            if (!r)
            {
                MessageBox.Show("Usuario no existe");
                return false;
            }
            else
            {
                if (SessionUser.currentUser.RolId == 1)
                {
                    Home home = new Home();
                    home.Visibility = Visibility.Visible;
                }
                else if (SessionUser.currentUser.RolId == 2)
                {
                    HomerSuper homerSuper = new HomerSuper();
                    homerSuper.Visibility = Visibility.Visible;
                }
                else
                {
                    HomeAdmin homeAdmin = new HomeAdmin();
                    homeAdmin.Visibility = Visibility.Visible;
                }
                return true;
            }
        }

        public Person infoUser(int idPerson)
        {
            return logServices.dataUSer(idPerson);
        }

        public bool updateUser(Person person)
        {
            return logServices.updateUser(person);
        }

        public bool updateUserPassword(User user)
        {
            return logServices.updateUserPassword(user);
        }
        public List<User> GetListUser()
        {
            return logServices.listUsers();
        }
    }
}
