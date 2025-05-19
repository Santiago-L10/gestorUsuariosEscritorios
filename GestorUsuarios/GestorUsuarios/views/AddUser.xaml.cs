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
    /// Lógica de interacción para AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        public AddUser()
        {
            InitializeComponent();
            AddRols();
            Estates();
            AddTime();
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            // Validar que no haya campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtNickname.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text) ||
                cmbRol.SelectedValue == null ||
                cmbEstado.SelectedValue == null ||
                cmbTiempoActividad.SelectedValue == null)
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, completa la información.");
                return;
            }

            string nombre = txtNombres.Text;
            string apellidos = txtApellidos.Text;
            string email = txtCorreo.Text;
            string nickname = txtNickname.Text;
            int age = int.TryParse(txtEdad.Text, out int parsedAge) ? parsedAge : 0;
            int rol = Convert.ToInt32(cmbRol.SelectedValue);
            bool estate = (bool)cmbEstado.SelectedValue;
            int activityTime = Convert.ToInt32(cmbTiempoActividad.SelectedValue);

            ControllerPerson controllerPerson = new ControllerPerson(new ServicesPersons(new DBPerson()));
            ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));
            SendEmails sendEmails = new SendEmails();

            // Crear y registrar usuario
            Person person = new Person(nombre, apellidos, age, email);
            controllerPerson.AddPerson(person);
            Person idPerson = controllerPerson.GetPerson(null, nombre);

            string pass = RandomPass();
            User user = new User(nickname, pass, activityTime, rol, idPerson.IdPerson, estate, DateTime.Now, DateTime.Now);
            controllerUser.AddUserController(user);

            sendEmails.addToDestination(idPerson.EmailPerson);
            sendEmails.contentEmail("Registro exitoso", true, idPerson.NamesPerson,
                $"Se ha creado de manera exitosa, sus credenciales son:\nUsuario: {nickname}\nContraseña: {pass}",
                SendEmails.templayEmail["Registro"]);
            sendEmails.sendEmail();

            MessageBox.Show("Usuario creado con éxito\nSe ha enviado al correo las credenciales");
        }


        public void Estates()
        {
            Dictionary<string, bool> estado = new Dictionary<string, bool>
            {
                {"Activo", true},
                {"Inactivo",false}
            };
            cmbEstado.ItemsSource = estado;
            cmbEstado.DisplayMemberPath = "Key";
            cmbEstado.SelectedValuePath = "Value";
        }

        private void AddRols()
        {
            ControllerRol controllerRol = new ControllerRol(new ServicesRol(new DBRol()));
            cmbRol.ItemsSource = controllerRol.GetRoles();
            cmbRol.DisplayMemberPath = "NameRol";
            cmbRol.SelectedValuePath = "IdRol";
        }

        private void AddTime()
        {
            Dictionary<string, int> time = new Dictionary<string, int>
            {
                {"30", 30000},
                {"15", 15000}
            };
            cmbTiempoActividad.ItemsSource = time;
            cmbTiempoActividad.DisplayMemberPath = "Key";
            cmbTiempoActividad.SelectedValuePath = "Value";
        }

        private string RandomPass()
        {
            Random random = new Random();
            string pass="";
            for (int i = 0; i < 4; i++)
            {
                int randomNumber = random.Next(0, 9);
                pass += randomNumber.ToString();
            }
            
            return pass;
        }
    }
}
