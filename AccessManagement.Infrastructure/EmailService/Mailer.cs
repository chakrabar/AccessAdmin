using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace AccessManagement.Infrastructure.EmailService
{
    public class Mailer
    {
        public static bool SendMail(string to, string from, string body, string subject, string attachmentPath)
        {
            try
            {
                var mail = CreateMail(to, from, body, subject, attachmentPath);
                var client = CreateMailClient();
                client.Send(mail);                
                return true;
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }

        private static SmtpClient CreateMailClient()
        {
            var mailServer = ConfigurationManager.AppSettings["SmtpServer"];
            var mailPort = ConfigurationManager.AppSettings["SmtpPort"];
            var user = ConfigurationManager.AppSettings["MailAccount"]; //TODO: remove from config
            var pwd = ConfigurationManager.AppSettings["MailPassword"];

            return new SmtpClient(mailServer, int.Parse(mailPort))
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(user, pwd),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
        }

        private static MailMessage CreateMail(string to, string from, string body, string subject, string attachmentPath)
        {
            var mail = new MailMessage();
            mail.Attachments.Add(new Attachment(attachmentPath));
            mail.Body = body;
            mail.From = new MailAddress(from);
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.To.Add(new MailAddress(to));
            return mail;
        }
    }
}
