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
    }
}
