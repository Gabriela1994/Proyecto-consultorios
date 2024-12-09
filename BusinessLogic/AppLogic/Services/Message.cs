using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BusinessLogic.AppLogic
{
    public class Message : IMessage
    {


        public void SendEmail(string subject, string body, string to)
        {
            try
            {
                EmailSettings settings = new EmailSettings();
                var fromEmail = settings.Username;
                var password = settings.Password;

                var message = new MailMessage();
                message.From = new MailAddress(fromEmail!);
                message.Subject = subject;
                message.To.Add(new MailAddress(to));
                message.Body = body;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = settings.Port,
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true
                };

                smtpClient.Send(message);

            }
            catch(Exception ex)
            {
                throw new Exception("No se pudo enviar el email", ex);
            }
        }
    }
}
