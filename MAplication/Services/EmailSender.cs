using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MAplication.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("worldenderaatrox0@gmail.com", "ZUzu8388!")
            };

            using (var mail = new MailMessage("worldenderaatrox0.Ingamedemo@gmail.com", email)
            {
                Subject = subject,
                Body = message
            })
            {
                await smtpClient.SendMailAsync(mail);
            }
        }
    }
}
