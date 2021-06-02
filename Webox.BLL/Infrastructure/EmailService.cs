using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace Webox.BLL.Infrastructure
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Розробник О.В.Митринюк", "webox.diploma@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("Шановний Користувачу!", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("webox.diploma@gmail.com", "123456qw_");
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
