using System.Windows;
using GestorUsuarios.controller;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;
using GestorUsuarios.services;
using GestorUsuarios.views;

namespace GestorUsuarios
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            /*
             * ##INSERT INTO roles (id, name) VALUES (1, "Usuario");
##INSERT INTO roles (id, name) VALUES (2, "Supervisor");
INSERT INTO roles (id, name) VALUES (3, "Admin");

             * DBPerson dBPerson = new DBPerson();
            DBUser dBUser = new DBUser();
            Person person = new Person("Feliep", "delga", 26, "email");
            User user = new User("pipejfdv", "1234", 10000, 1, 1, true, DateTime.Now, DateTime.Now);
            dBPerson.SetPerson(person);
            dBUser.SetUser(user);*/

        }
        private void Home(object sender, RoutedEventArgs e)
        {
            DBUser dBUser = new DBUser();
            DBPerson dBPerson = new DBPerson();
            LoginServices loginServices = new LoginServices(new DBUser(), new DBPerson());
            HomeController controller = new HomeController(loginServices);
            string u = userText.Text;
            string p = passBox.Password;
            
            if (u.Length == 0 || p.Length == 0)
            {
                MessageBox.Show("Campos vacios");
            }
            else
            {
                bool r = controller.acces(u, p);
                if (r)
                {
                    this.Visibility = Visibility.Collapsed;
                }
            }
                
        }
        
    }
}