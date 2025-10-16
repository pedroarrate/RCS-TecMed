using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace RCSTecMed_Controll
{
    public class EmailNotifier
    {
        private readonly string senderEmail;
        private readonly string senderPassword;
        private readonly string smtpHost;
        private readonly int smtpPort;

        public EmailNotifier()
        {
            senderEmail = ConfigurationManager.AppSettings["SenderEmail"];
            senderPassword = ConfigurationManager.AppSettings["SenderPassword"];
            smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        }

        public bool SendEmail(List<string> recipients, string subject, string body, bool isHtml = true, List<Attachment> attachments = null) // ENVIAR EMAIL A VARIOS DESTINATARIOS CON ARCHIVOS ADJUNTOS
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml
                };

                foreach (var recipient in recipients)
                    message.To.Add(recipient);

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                        message.Attachments.Add(attachment);
                }

                using (var client = new SmtpClient(smtpHost, smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    client.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                return false;
            }
        }

        public bool EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtml = true) //ENVIAR EMAIL A 1 DESTINATARIO SIN ARCHIVOS ADJUNTOS
        {
            try
            {
                var correo = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = asunto,
                    Body = mensaje,
                    IsBodyHtml = esHtml
                };

                correo.To.Add(destinatario);

                using (var clienteSmtp = new SmtpClient(smtpHost, smtpPort))
                {
                    clienteSmtp.EnableSsl = true;
                    clienteSmtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    clienteSmtp.Send(correo);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                return false;
            }
        }
    }
}
