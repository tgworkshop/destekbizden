using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform
{
    public class MailService
    {
        public void SendEmail(string subject, string from, string fromName, Dictionary<string,string> recipients, string body)
        {
            // Create the smtp client
            SmtpClient smtpClient = new SmtpClient();

            // Set credentials
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTP.Username"], ConfigurationManager.AppSettings["SMTP.Password"]);

            // Set the client
            smtpClient.Host = ConfigurationManager.AppSettings["SMTP.Host"];
            smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTP.Port"]);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            // Create the message
            var message = new MailMessage();
            message.Subject = subject;
            message.From = new MailAddress(from, fromName);
            message.IsBodyHtml = true;
            message.Body = body;

            // Add recipients
            recipients.ToList().ForEach(kvp => message.To.Add(new MailAddress(kvp.Key, kvp.Value)));

            try
            {
                // Send the email
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
