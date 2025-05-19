using GestorUsuarios.models;
using GestorUsuarios.services;
using System.Windows;

namespace GestorUsuarios
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            bool inactivo;
            UserService servicio = new UserService();
            User usuario = servicio.ValidarCredenciales(username, password, out inactivo);

            if (usuario != null)
            {
                MainWindow main = new MainWindow(usuario);
                main.Show();
                this.Close();
            }
            else if (inactivo)
            {
                MessageBox.Show("Tu usuario está inactivo. Contacta al administrador.", "Cuenta inactiva", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Credenciales inválidas", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AbrirRegistro_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().ShowDialog();
        }
    }
}
