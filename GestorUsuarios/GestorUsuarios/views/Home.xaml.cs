using System.Windows;

namespace GestorUsuarios.views
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            VistaFrame.Navigate(new ListTask());
        }

        public void ListToTask(object sender, RoutedEventArgs e)
        {
            VistaFrame.NavigationService.Navigate(new ListTask());
        }

        public void dataUser(object sender, RoutedEventArgs e)
        {
            VistaFrame.NavigationService.Navigate(new DatosUsuario());
        }

        public void CerrarSesion(object sender, RoutedEventArgs e)
        {
            this.Close(); // Cierra la ventana actual

            // Encuentra la ventana principal ya existente
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow main)
                {
                    main.Visibility = Visibility.Visible;
                    main.Show(); // Asegura que se muestre correctamente
                    return;
                }
            }

            // Si no se encuentra una ventana abierta, crear una nueva
            MainWindow nuevaMain = new MainWindow();
            nuevaMain.Show();
        }
    }
}
