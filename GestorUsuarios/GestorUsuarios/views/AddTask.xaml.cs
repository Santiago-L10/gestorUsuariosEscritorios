using System.Windows;
using System.Windows.Controls;
using GestorUsuarios.controller;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;
using GestorUsuarios.services;

namespace GestorUsuarios.views
{
    /// <summary>
    /// Lógica de interacción para AddTask.xaml
    /// </summary>
    public partial class AddTask : Page
    {

        public AddTask()
        {

            InitializeComponent();
            loadUsers();
            priorityEmail();
            templaysEmailBox();
        }

        public void createTask(object sender, RoutedEventArgs e)
        {
            ServicesUsers servicesUsers = new ServicesUsers(new DBUser(), new DBPerson());
            ControllerUser controllerUser = new ControllerUser(servicesUsers);
            SendEmails sendEmails = new SendEmails();

            string nameTask = NameTask.Text;
            
            string especifications = specifications.Text;
            string plantillaSeleccionada = (string)templayEmailComboBox.SelectedValue;
            User user = (User)userComboBox.SelectedItem;
            if (nameTask.Length == 0 || user == null || priorityComboBox.SelectedValue == null)
            {
                MessageBox.Show("no has completado todos los campos");
                return;
            }
            bool priority = (bool)priorityComboBox.SelectedValue;
            Person person = controllerUser.infoUser(user.PersonId);
            TaskModel taskModel = new TaskModel(nameTask, user.Nickname, false, especifications, DateTime.Now);
            TaskGlobal.Tasks.Add(taskModel);
            MessageBox.Show("PLANTILLLA :" + plantillaSeleccionada);
            sendEmails.addToDestination(person.EmailPerson);
            sendEmails.contentEmail("Asignación de tarea", priority, user.Nickname, especifications, plantillaSeleccionada);
            sendEmails.sendEmail();
            MessageBox.Show("Tarea asignada y enviada al correo");
            
        }

        public void priorityEmail()
        {
            Dictionary<string, bool> priority = new Dictionary<string, bool>
            {
                {"Alta", true},
                {"Normal",false}
            };
            priorityComboBox.ItemsSource = priority;
            priorityComboBox.DisplayMemberPath = "Key";
            priorityComboBox.SelectedValuePath = "Value";
        }

        public void templaysEmailBox()
        {
            templayEmailComboBox.ItemsSource = SendEmails.templayEmail;
            templayEmailComboBox.DisplayMemberPath = "Key";
            templayEmailComboBox.SelectedValuePath = "Value";
        }
        public void loadUsers()
        {
            ServicesUsers servicesUsers = new ServicesUsers(new DBUser(), new DBPerson());
            ControllerUser controllerUser = new ControllerUser(servicesUsers);
            List<User> listUsers = controllerUser.GetListUser();
            if (listUsers != null && listUsers.Count > 0)
            {
                List<User> filterUsers = listUsers.Where(user => user.RolId != 2).ToList();
                userComboBox.ItemsSource = filterUsers;
                userComboBox.DisplayMemberPath = "Nickname";

            }
            else
            {
                MessageBox.Show("No hay usuarios disponibles");
            }
        }
    }
}
