using System.Windows;
using System.Windows.Controls;
using GestorUsuarios.services;

namespace GestorUsuarios.views
{
    /// <summary>
    /// Lógica de interacción para TaskSuper.xaml
    /// </summary>
    public partial class TaskSuper : Page
    {
        public TaskSuper()
        {
            InitializeComponent();
            SupervisorGrid.ItemsSource = TaskGlobal.Tasks;

        }
        public void realoadList(object sender, RoutedEventArgs e)
        {
            SupervisorGrid.ItemsSource = null; // Limpia la referencia anterior
            SupervisorGrid.ItemsSource = TaskGlobal.Tasks;
        }
    }
}
