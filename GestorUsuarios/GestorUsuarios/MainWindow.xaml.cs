using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;
using GestorUsuarios.services;

namespace GestorUsuarios
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*SendEmails sendEmails = new SendEmails();

            string plantilla = SendEmails.templayEmail["updatePassword"];
            //sendEmails.addFilesToEmail();
            sendEmails.addToDestination("pedroclemente2209@gmail.com");
            sendEmails.contentEmail("Actualizar contraseña", true, "Felipe", "Es necesario que actualice la contraseña", plantilla);
            //sendEmails.sendEmail();*/
            DBPerson sd = new DBPerson();
            Person person = new Person(1,"Felipe", "Delga", 26, "pi@gm.com");
            sd.SetPerson(person);
        }
    }
}