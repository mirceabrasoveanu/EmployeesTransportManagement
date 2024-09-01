using System.Net.Mail;
using System.Net;

namespace EmployeesTransportManagement
{
    public class EmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("facultate22002@gmail.com", "hagy mchg zzub lgse"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("facultate22002@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }

}
