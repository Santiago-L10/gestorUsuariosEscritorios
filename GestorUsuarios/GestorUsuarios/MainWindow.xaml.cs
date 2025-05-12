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
            SendEmails sendEmails = new SendEmails();
            //sendEmails.addFilesToEmail();
            sendEmails.addToDestination("juampi_03_33@hotmail.com");
            sendEmails.contentEmail("prueba Subject","Prueba contenido <h1>Electrico</h1>", false);
            sendEmails.sendEmail();
        }
    }
}