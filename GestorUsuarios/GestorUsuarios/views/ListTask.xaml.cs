using System.Windows.Controls;
using GestorUsuarios.services;
using TaskModel = GestorUsuarios.models.TaskModel;

namespace GestorUsuarios.views
{
    /// <summary>
    /// Lógica de interacción para ListTask.xaml
    /// </summary>
    public partial class ListTask : Page
    {
        public string userCurrent = SessionUser.currentUser.Nickname;
        public ListTask()
        {
            InitializeComponent();
            LoadUserTaks(SessionUser.currentUser.Nickname);
        }

        private void LoadUserTaks(string user)
        {
            TareasGrid.ItemsSource = TaskGlobal.GetTasksForUser(user);
        }
        private void SaveTaks(object sender, EventArgs e) {
            var selectedTask = (TaskModel)TareasGrid.SelectedItem;
            if (selectedTask != null) {
                TaskGlobal.MarkTaskCompleted(selectedTask.Name, userCurrent);
                LoadUserTaks(userCurrent);
            }
        }
    }
}
