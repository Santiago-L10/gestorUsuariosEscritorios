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

namespace GestorUsuarios.views
{
    /// <summary>
    /// Lógica de interacción para HomerSuper.xaml
    /// </summary>
    public partial class HomerSuper : Window
    {
        public HomerSuper()
        {
            InitializeComponent();
            VistaFrame.NavigationService.Navigate(new TaskSuper());
        }
        public void ListToTask(object sender, RoutedEventArgs e)
        {
            VistaFrame.NavigationService.Navigate(new ListTask());
        }
    }
}
