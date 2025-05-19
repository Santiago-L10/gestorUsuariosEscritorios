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
            }).ToList();
            GridUsers.ItemsSource = userData;
        }
    }
}
