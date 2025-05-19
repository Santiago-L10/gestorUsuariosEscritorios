using GestorUsuarios.services;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Windows;

namespace GestorUsuarios
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombre = NombreTextBox.Text;
                string apellido = ApellidoTextBox.Text;
                int edad = int.Parse(EdadTextBox.Text);
                string email = EmailTextBox.Text;
                string nickname = NicknameTextBox.Text;
                string password = PasswordBox.Password;

                var cmdPerson = new MySqlCommand("INSERT INTO persons (name, lastname, age, email) VALUES (@n, @a, @ed, @e)", new DBConnection().Open());
                cmdPerson.Parameters.AddWithValue("@n", nombre);
                cmdPerson.Parameters.AddWithValue("@a", apellido);
                cmdPerson.Parameters.AddWithValue("@ed", edad);
                cmdPerson.Parameters.AddWithValue("@e", email);
                cmdPerson.ExecuteNonQuery();
                int personId = (int)cmdPerson.LastInsertedId;

                var cmdUser = new MySqlCommand("INSERT INTO users (nickname, password, idRol, idPerson, estado, LastLoginDate, CreationDate) VALUES (@nick, @pass, 2, @pid, 1, NOW(), NOW())", cmdPerson.Connection);
                cmdUser.Parameters.AddWithValue("@nick", nickname);
                cmdUser.Parameters.AddWithValue("@pass", password);
                cmdUser.Parameters.AddWithValue("@pid", personId);
                cmdUser.ExecuteNonQuery();

                cmdUser.Connection.Close();

                MessageBox.Show("Registro exitoso. Ahora puedes iniciar sesión.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }
    }
}
