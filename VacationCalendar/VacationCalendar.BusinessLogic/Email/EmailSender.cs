using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace VacationCalendar.BusinessLogic.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            string mail = File.ReadAllText("C:\\VacationCalendarEmail\\email.txt");
            string pwd = File.ReadAllText("C:\\VacationCalendarEmail\\pwd.txt");        

            var client = new SmtpClient("smtp.mail.yahoo.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pwd)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail, to: email, subject, message));

        }
    }
}
