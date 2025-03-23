using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.EmailServices
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        private readonly IConfiguration _configuration = configuration;
        public Task SendEmailAsync(string email, string subject, string body)
        {
            var fromEmail = _configuration.GetSection("EmailSending")["email"];
            var password = _configuration.GetSection("EmailSending")["password"];
            // Validate configuration values
            if (string.IsNullOrEmpty(fromEmail))
            {
                throw new ArgumentNullException(nameof(fromEmail), "Email configuration 'email' is missing or empty.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "Email configuration 'password' is missing or empty.");
            }

            // Log the fromEmail for debugging
            Console.WriteLine($"Sending email from: {fromEmail}");
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, password)
            };
            var mailMessage = new MailMessage(from: fromEmail, to: email, subject: subject, body: body);
            return client.SendMailAsync(mailMessage);
        }
    }
}
