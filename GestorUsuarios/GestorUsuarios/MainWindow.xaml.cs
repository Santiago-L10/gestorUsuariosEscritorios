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
             * ALTER TABLE persons ADD COLUMN email VARCHAR(255);
             * ##INSERT INTO roles (id, name) VALUES (1, "Usuario");
##INSERT INTO roles (id, name) VALUES (2, "Supervisor");
INSERT INTO roles (id, name) VALUES (3, "Admin");

            DBPerson dBPerson = new DBPerson();
            DBUser dBUser = new DBUser();
            ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));
            
            Person person = new Person("Luisa", "Echeveri", 25, "juampi_03_33@hotmail.com");
            
            User user = new User("Lu", "1234", 10000, 3, 4, true, DateTime.Now, DateTime.Now);
            dBPerson.SetPerson(person);
            dBUser.SetUser(user);*/
        }
        private void Home(object sender, RoutedEventArgs e)
        {
            DBUser dBUser = new DBUser();
            DBPerson dBPerson = new DBPerson();
            ServicesUsers loginServices = new ServicesUsers(new DBUser(), new DBPerson());
            ControllerUser controller = new ControllerUser(loginServices);
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

        private void RecoveryData(object sender, RoutedEventArgs e)
        {
            string userRecovery = userText.Text;
            if(userRecovery.Length == 0)
            {
                MessageBox.Show("Ingresa tu usuario");
                return;
            }
            ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));
            SendEmails sendEmails = new SendEmails();
            User user = controllerUser.GetUser(null, userRecovery);
            Person person = controllerUser.infoUser(user.PersonId);
            
            sendEmails.addToDestination("juampi_03_33@hotmail.com");
            sendEmails.contentEmail("Recuperación de credenciales", true, "Admin", $"Solicita recuperación de credenciales\nusuario: {person.NamesPerson}", SendEmails.templayEmail["Actualizar contraseña"]);
            sendEmails.sendEmail();
            MessageBox.Show("Solicitud generada");
        }


    }
}