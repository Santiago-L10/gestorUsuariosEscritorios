using GestorUsuarios.models;
using GestorUsuarios.services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
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
