using GestorUsuarios.models;
using GestorUsuarios.services;
using System.Collections.Generic;
using System.Windows;
using GestorUsuarios.controller;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;
using GestorUsuarios.services;
using GestorUsuarios.views;

namespace GestorUsuarios
{
    public partial class MainWindow : Window
    {
        private User UsuarioLogueado;

        
        public MainWindow(User usuario)
        {
            InitializeComponent();
            UsuarioLogueado = usuario;
            Loaded += MainWindow_Loaded;
            
            /*SendEmails sendEmails = new SendEmails();

            string plantilla = SendEmails.templayEmail["updatePassword"];
            //sendEmails.addFilesToEmail();
            sendEmails.addToDestination("pedroclemente2209@gmail.com");
            sendEmails.contentEmail("Actualizar contraseña", true, "Felipe", "Es necesario que actualice la contraseña", plantilla);
            //sendEmails.sendEmail();*/
            DBPerson sd = new DBPerson();
            Person person = new Person(1,"Felipe", "Delga", 26, "pi@gm.com");
            sd.SetPerson(person);

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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            /*SendEmails sendEmails = new SendEmails();

            string plantilla = SendEmails.templayEmail["updatePassword"];
            //sendEmails.addFilesToEmail();
            sendEmails.addToDestination("pedroclemente2209@gmail.com");
            sendEmails.contentEmail("Actualizar contraseña", true, "Felipe", "Es necesario que actualice la contraseña", plantilla);
            //sendEmails.sendEmail();*/
            /*DBPerson sd = new DBPerson();
            Person person = new Person(1,"Felipe", "Delga", 26, "pi@gm.com");
            sd.SetPerson(person);*/

            string rolUsuario = UsuarioLogueado.RolNombre.ToLower();

            if (rolUsuario == "admin")
            {
                VistaAdmin.Visibility = Visibility.Visible;
                VistaUsuario.Visibility = Visibility.Collapsed;

                UserService servicio = new UserService();
                List<User> usuarios = servicio.ObtenerUsuarios();
                UsuariosDataGrid.ItemsSource = usuarios;

                MainGrid.Background = new SolidColorBrush(Colors.LightSkyBlue);
            }
            else if (rolUsuario == "usuario")
            {
                VistaAdmin.Visibility = Visibility.Collapsed;
                VistaUsuario.Visibility = Visibility.Visible;

                BienvenidaTextBlock.Text = $"Hola, {UsuarioLogueado.Nombre} 👋";
                MainGrid.Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
            }
            else
            {
                VistaAdmin.Visibility = Visibility.Collapsed;
                VistaUsuario.Visibility = Visibility.Visible;
                MainGrid.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void RegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().ShowDialog();
        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void EditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null && int.TryParse(boton.Tag.ToString(), out int idUsuario))
            {
                EditarUsuarioWindow editar = new EditarUsuarioWindow(idUsuario);
                editar.ShowDialog();

                UserService servicio = new UserService();
                UsuariosDataGrid.ItemsSource = servicio.ObtenerUsuarios();
            }
        }

        private void EliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null && int.TryParse(boton.Tag.ToString(), out int idUsuario))
            {
                if (idUsuario == UsuarioLogueado.Id)
                {
                    MessageBox.Show("No puedes eliminar tu propio usuario.", "Acción no permitida", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    new UserService().EliminarUsuario(idUsuario);
                    MessageBox.Show("Usuario eliminado correctamente.");

                    UserService servicio = new UserService();
                    UsuariosDataGrid.ItemsSource = servicio.ObtenerUsuarios();
                }
            }
        }
    }
}