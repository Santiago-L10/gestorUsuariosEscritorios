using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestorUsuarios.controller;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;
using GestorUsuarios.services;

namespace GestorUsuarios.views
{
    /// <summary>
    /// Lógica de interacción para DatosUsuario.xaml
    /// </summary>
    public partial class DatosUsuario : Page
    {
        private User usuarioActual;
        public DatosUsuario()
        {
            InitializeComponent();
            CargarDatosUsuario();
        }
        private void CargarDatosUsuario()
        {

            LoginServices loginServices = new LoginServices(new DBUser(), new DBPerson());
            HomeController controller = new HomeController(loginServices);
            usuarioActual = SessionUser.currentUser; // Obtener el usuario en sesión

            Person person = controller.infoUser(usuarioActual.PersonId);
            if (usuarioActual != null)
            {
                NicknameTextBox.Text = usuarioActual.Nickname;
                NombreTextBox.Text = person.NamesPerson;
                ApellidoTextBox.Text = person.LastNamesPerson;
                CorreoTextBox.Text = person.EmailPerson;
                EdadTextBox.Text = person.AgePerson.ToString();
                ContrasenaBox.Password = usuarioActual.PasswordHash;
            }
        }

        private void UpdateInfoUser(object sender, RoutedEventArgs e)
        {
            if (usuarioActual == null)
            {
                MessageBox.Show("Error: No hay usuario en sesión.");
                return;
            }

            Person person = new Person
            {
                IdPerson = usuarioActual.PersonId,
                NamesPerson = NombreTextBox.Text,
                LastNamesPerson = ApellidoTextBox.Text,
                EmailPerson = CorreoTextBox.Text,
                AgePerson = int.Parse(EdadTextBox.Text)
            };

            User user = new User
            {
                IdUser = usuarioActual.IdUser,
                Nickname = NicknameTextBox.Text,
                RolId = usuarioActual.RolId,
                PersonId = usuarioActual.PersonId,
                PasswordHash = ContrasenaBox.Password
            };
            
            LoginServices loginServices = new LoginServices(new DBUser(), new DBPerson());
            HomeController controller = new HomeController(loginServices);

            bool r1 = controller.updateUser(person);
            bool r2 = controller.updateUserPassword(user);

            if (r1 && r2)
            {
                MessageBox.Show("Usuario actualizado correctamente");
            }
            else
            {
                MessageBox.Show("Error al actualizar el usuario");

            }
        }
    }
}
