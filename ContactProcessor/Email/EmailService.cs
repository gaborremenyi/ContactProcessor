using System.Net.Mail;

namespace ContactProcessor.Email
{
    public class EmailService : IEmailService
    {
        public void Send(string hostName, string emailFrom, string emailTo, string subject, string body)
        {
            using (var message = new MailMessage(emailFrom, emailTo, subject, body))
            {
                using (SmtpClient client = new SmtpClient(hostName))
                {
                    client.UseDefaultCredentials = true;
                    client.Send(message);
                }
            }
        }
    }
}