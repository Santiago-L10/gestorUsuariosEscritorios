using GestorUsuarios.models;
using GestorUsuarios.services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GestorUsuarios
{
    public partial class EditarUsuarioWindow : Window
    {
        private int IdUsuario;
        private User usuarioOriginal;

        public EditarUsuarioWindow(int idUsuario)
        {
            InitializeComponent();
            IdUsuario = idUsuario;
            CargarDatos();
        }

        private void CargarDatos()
        {
            usuarioOriginal = new UserService().ObtenerUsuarios()
                                               .Find(u => u.Id == IdUsuario);

            if (usuarioOriginal != null)
            {
                NombreTextBox.Text = usuarioOriginal.Nombre;
                EmailTextBox.Text = usuarioOriginal.Email;
                PasswordBox.Password = usuarioOriginal.PasswordHash;
                ActivoCheckBox.IsChecked = usuarioOriginal.Estado;

                RolComboBox.SelectedItem = RolComboBox.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(i => i.Content.ToString().ToLower() == usuarioOriginal.RolNombre.ToLower());
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            string rolSeleccionado = ((ComboBoxItem)RolComboBox.SelectedItem)?.Content.ToString() ?? "usuario";
            int rolId = rolSeleccionado == "admin" ? 1 : 2;

            var usuario = new User(
                IdUsuario,
                NombreTextBox.Text,
                EmailTextBox.Text,
                PasswordBox.Password,
                30,
                usuarioOriginal.FechaCreacion,
                rolId
            )
            {
                Estado = ActivoCheckBox.IsChecked == true,
                Rol = new Rol(rolSeleccionado)
            };

            new UserService().ActualizarUsuario(usuario);
            MessageBox.Show("Usuario actualizado");
            this.Close();
        }
    }
}
