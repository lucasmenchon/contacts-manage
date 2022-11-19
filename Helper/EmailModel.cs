using System.Net;
using System.Net.Mail;

namespace ContactsManage.Helper
{
    public class EmailModel : IEmail
    {
        //private readonly IConfiguration _configuration;

        //public EmailModel(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public bool SendEmail(string email, string subject, string message)
        {
            try
            {
                //tudo comentado neste bloco de codigo e caso seja preciso usar appsettings.json e mais de 1 smtp
                //string host = _configuration.GetValue<string>("SMTP:Host");
                //string nome = _configuration.GetValue<string>("SMTP:Nome");
                //string username = _configuration.GetValue<string>("SMTP:Username");
                //string senha = _configuration.GetValue<string>("SMTP:Senha");
                //int porta = _configuration.GetValue<int>("SMTP:Porta");

                string host = "smtp.office365.com";
                int port = 587;
                string systemEmail = "developer@luccas.dev";
                string passwordEmail = "Development!4132@";
                string systemName = "Contacts System";

                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(systemEmail, systemName)
                };

                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                using (SmtpClient smtpClient = new SmtpClient(host, port))
                {                   
                    smtpClient.Credentials = new NetworkCredential(systemEmail, passwordEmail);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }      
    }
}
