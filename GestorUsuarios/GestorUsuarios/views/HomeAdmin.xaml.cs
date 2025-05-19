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
using GestorUsuarios.models;
using GestorUsuarios.models.conex;
using GestorUsuarios.services;

namespace GestorUsuarios.views
{
    /// <summary>
    /// Lógica de interacción para HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Window
    {
        public HomeAdmin()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(new ListaUsuariosAdmin());
        }

        private void OpenUserList(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ListaUsuariosAdmin());
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
