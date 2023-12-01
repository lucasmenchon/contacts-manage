using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace ContactsManage.Helper
{
    public class EmailModel : IEmail
    {
        private readonly IConfiguration _configuration;

        public EmailModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(string email, string subject, string message)
        {
            try
            {
                string host = _configuration.GetValue<string>("smtp:host");
                string name = _configuration.GetValue<string>("smtp:name");
                string username = _configuration.GetValue<string>("smtp:username");
                string password = _configuration.GetValue<string>("smtp:password");
                int port = _configuration.GetValue<int>("smtp:port");

                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(username, name)
                };

                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                using (SmtpClient smtpClient = new SmtpClient(host, port))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(username, password);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}