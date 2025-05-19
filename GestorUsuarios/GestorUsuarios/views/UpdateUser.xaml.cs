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
using System.Windows.Shapes;
using GestorUsuarios.controller;
using GestorUsuarios.models.conex;
using GestorUsuarios.models;
using GestorUsuarios.services;

namespace GestorUsuarios.views
{
    /// <summary>
    /// Lógica de interacción para UpdateUser.xaml
    /// </summary>
    public partial class UpdateUser : Window
    {
        private int idUsuario;

        public User usuarioActual;
        public UpdateUser(int user)
        {
            InitializeComponent();
            
            ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));
            usuarioActual = controllerUser.GetUser(user, null);
            CargarDatosUsuario(usuarioActual);
        }

        private void CargarDatosUsuario(User usuarioActual)
        {
            ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));

            if (usuarioActual != null)
            {
                Person person = controllerUser.infoUser(usuarioActual.PersonId);
                NombreTextBox.Text = person.NamesPerson;
                ApellidoTextBox.Text = person.LastNamesPerson;
                CorreoTextBox.Text = person.EmailPerson;
                EdadTextBox.Text = person.AgePerson.ToString();
                EstadoComboBox.SelectedItem = usuarioActual.Estado ? "Activo" : "Inactivo";

                // Validar que el rol existe antes de asignarlo
                DBRol dBRol = new DBRol();
                var rolUsuario = dBRol.GetRol(usuarioActual.RolId);
                if (rolUsuario != null)
                {
                    RolComboBox.SelectedItem = rolUsuario.NameRol;
                }
                else
                {
                    MessageBox.Show("Error: No se encontró el rol asociado al usuario.");
                }
            }
        }


        private void actualizar(User UsuarioOptenido)
        {
            if (UsuarioOptenido == null)
            {
                MessageBox.Show("Error: No hay usuario en sesión.");
                return;
            }

            // Actualizar datos personales
            Person person = new Person
            {
                IdPerson = UsuarioOptenido.PersonId,
                NamesPerson = NombreTextBox.Text,
                LastNamesPerson = ApellidoTextBox.Text,
                EmailPerson = CorreoTextBox.Text,
                AgePerson = int.Parse(EdadTextBox.Text)
            };

            // Actualizar datos de usuario (sin incluir contraseña)
            User user = new User
            {
                IdUser = UsuarioOptenido.IdUser,
                Nickname = UsuarioOptenido.Nickname,
                RolId = RolComboBox.SelectedValue != null ? Convert.ToInt32(RolComboBox.SelectedValue) : UsuarioOptenido.RolId,
                PersonId = UsuarioOptenido.PersonId,
                Estado = EstadoComboBox.SelectedItem.ToString() == "Activo"
            };

            // Controladores para actualizar la información en la base de datos
            ServicesUsers loginServices = new ServicesUsers(new DBUser(), new DBPerson());
            ControllerUser controller = new ControllerUser(loginServices);

            bool r1 = controller.updateUser(person);
            bool r2 = controller.updateUser(user);

            if (r1 && r2)
            {
                MessageBox.Show("Usuario actualizado correctamente.");
            }
            else
            {
                MessageBox.Show("Error al actualizar el usuario.");
            }
        }
        private void UpdateInfoUser(object sender, RoutedEventArgs e)
        {
            ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));
            usuarioActual = controllerUser.GetUser(idUsuario, null); 
            actualizar(usuarioActual);
        }
    }

}
