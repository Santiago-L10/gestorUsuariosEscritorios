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
using GestorUsuarios.models.conex;
using GestorUsuarios.models;
using GestorUsuarios.services;

namespace GestorUsuarios.views
{
    /// <summary>
    /// Lógica de interacción para ListaUsuariosAdmin.xaml
    /// </summary>
    public partial class ListaUsuariosAdmin : Page
    {
        public ListaUsuariosAdmin()
        {
            InitializeComponent();
            listUsers();
            listaUserNames();
        }

        public void listUsers()
        {
            ControllerPerson controllerPerson = new ControllerPerson(new ServicesPersons(new DBPerson()));
            ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));
            List<Person> persons = controllerPerson.ListUsers();
            List<User> users = controllerUser.GetListUser();

            var userData = users.Select(user => new
            {
                Id = user.IdUser,
                Nombres = persons.FirstOrDefault(p => p.IdPerson == user.PersonId)?.NamesPerson ?? "Desconocido",
                Apellidos = persons.FirstOrDefault(p => p.IdPerson == user.PersonId)?.LastNamesPerson ?? "Desconocido",
                Email = persons.FirstOrDefault(p => p.IdPerson == user.PersonId)?.EmailPerson ?? "Desconocido",
                Edad = persons.FirstOrDefault(p => p.IdPerson == user.PersonId)?.AgePerson ?? 0,
                Estado = user.Estado ? "Activo" : "Inactivo",
                Editar = "Editar", // Texto del botón
                Eliminar = "Eliminar" // Texto del botón
            }).ToList();

            GridUsers.ItemsSource = userData;
        }

        private void SelectUsuario(object sender, RoutedEventArgs e)
        {
            int idUSer = (int)FiltroComboBox.SelectedValue;
            UpdateUser update = new UpdateUser(idUSer);
            update.Visibility = Visibility.Visible;
        }

        private void listaUserNames()
        {
            ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));
            List<User> users = controllerUser.GetListUser();
            FiltroComboBox.ItemsSource = users;
            FiltroComboBox.DisplayMemberPath = "Nickname";
            FiltroComboBox.SelectedValuePath = "IdUser";
        }


        private void EliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            int idUSer = (int)FiltroComboBox.SelectedValue;

            ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));
            User usuarioSeleccionado = controllerUser.GetUser(idUSer);
            if (usuarioSeleccionado != null)
            {
                MessageBoxResult result = MessageBox.Show($"¿Eliminar usuario {usuarioSeleccionado.Nickname}?",
                                                          "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    DBUser dBUser = new DBUser();
                    DBPerson dDBPerson = new DBPerson();
                    int id = usuarioSeleccionado.PersonId;
                    int idu = usuarioSeleccionado.IdUser;
                    dDBPerson.DeletePerson(id);
                    dBUser.DeleteUser(idu);
                    
                    MessageBox.Show("Usuario eliminado correctamente.");
                }
            }
        }
    }
}
