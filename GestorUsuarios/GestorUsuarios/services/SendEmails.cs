using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using Microsoft.Win32;

namespace GestorUsuarios.services
{
    internal class SendEmails
    {
        private string emailAuthor = "pipejfdv@gmail.com";
        private string password = "geyp tjyu fghx ";
        private string alias = "EquipoTaller";
        private string[] files;
        private MailMessage email;
        public static List<string> listDestination = new List<string>();

        public void addToDestination(string destination)
        {
            listDestination.Add(destination);
        }
        public void contentEmail(string subject, bool priority, string nameRecipient, string textCustomized) {

            //string routeTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templateEmails", "plantillaBasica.html");
            string htmlContent = CargarPlantilla("s", nameRecipient, textCustomized);
            email = new MailMessage();
            email.From = new MailAddress(emailAuthor, alias, System.Text.Encoding.UTF8);
            for (int i = 0; i < listDestination.Count; i++) { 
                email.To.Add(listDestination[i]);
            }
            email.Subject = subject;
            //cuerpo del correo 
            email.Body = htmlContent;
            //agregar tipo de contenido html
            email.IsBodyHtml = true;
            //escoger la prioridad del correo (High,Normal)
            if (priority){
                email.Priority = MailPriority.High;
            }
            else{
                email.Priority = MailPriority.Normal;
            }
            //continuar si no hay archivos
            if (files != null)
            {   //adjuntar archivos
                for (int i = 0; i < files.Length; i++)
                {
                    email.Attachments.Add(new Attachment(files[i]));
                }
            }
            
        }

        public void sendEmail() {
            try {
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new NetworkCredential(emailAuthor, password);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                { return true; };
                smtp.EnableSsl = true;
                smtp.Send(email);
                MessageBox.Show("Envio correcto");
            //borrar información
            }
            catch (Exception e) {
                MessageBox.Show("Fallo el envio: "+e.Message);
            }
        }
        public void addFilesToEmail()//recomendable usar private
        {
            var names = "";//nombres para los archivos
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Adjuntar archivos";

            //obtener rutas de los archivos que se quieren adjuntar
            if (openFileDialog.ShowDialog() == true) { 
                files = openFileDialog.FileNames;
            }
            //Recorrerer el nombre de los archivos
            for (int i = 0; i < files.Length; i++) { 
                names += files[i]+"\n";
            }
            //asiganar label para adjuntar los nombres
            //labar.Text = names;
        }

        public string CargarPlantilla(string routeTemplate, string nameRecipient, string textCustomized)
        {
            //ruta del archivo
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\")); // Retroceder hasta el nivel del proyecto
            string newPath = Path.Combine(projectPath, "templateEmails", "plantillaBasica.html");
            string htmlContent = File.ReadAllText(newPath);
            //remplazar contenido
            htmlContent = htmlContent.
                Replace("{Nombre}", nameRecipient).
                Replace("{TextoPersonalizado}", textCustomized);
            return htmlContent;
        }

            /*sendEmails.addFilesToEmail();
            sendEmails.addToDestination("luisaecheverri91@outlook.com");
            sendEmails.contentEmail("Registro exitoso", true, "Luisa", "Pasaba a decirte que te amo y que sueñes con los angelitos");
            sendEmails.sendEmail();*/
    }
}
