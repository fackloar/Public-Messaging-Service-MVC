using MailKit.Net.Smtp;
using MimeKit;
using System.IO;

namespace MessagingService.Models
{
    public class EmailService
    {
        private string _email;
        private string _password;
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin", ""));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (StreamReader sr = File.OpenText(@"credentials.txt"))
            {
                string loginString = "";
                loginString = sr.ReadLine();
                string passwordString = "";
                passwordString = sr.ReadLine();

                _email = loginString.Split("Login: ")[1];
                _password = passwordString.Split("Password: ")[1];
            }


            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(_email, _password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
