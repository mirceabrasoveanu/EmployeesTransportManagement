using System.Net.Mail;
using System.Net;

namespace EmployeesTransportManagement
{
    public class EmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient("smtp.example.com")
            {
                Credentials = new NetworkCredential("your-email@example.com", "your-password"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("your-email@example.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }

}
