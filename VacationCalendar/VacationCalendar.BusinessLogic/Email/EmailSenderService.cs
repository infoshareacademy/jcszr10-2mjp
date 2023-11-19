using System.Net;
using System.Net.Mail;

namespace VacationCalendar.BusinessLogic.Email
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string reciver, string employeeEmail)
        {
            string[] emailConfig = File.ReadAllLines("C:\\VacationCalendarEmail\\emailconfig.txt");
            string mail = emailConfig[0].ToString();
            string pwd = emailConfig[1].ToString();

            var client = new SmtpClient("smtp.mail.yahoo.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pwd)
            };

            string subject = "Nowy wniosek urlopowy";
            string message = $"Pracownik {employeeEmail} wygenerował nowy wniosek urlopowy.";

            return client.SendMailAsync(
                new MailMessage(from: mail, to: reciver, subject, message));
        }
    }
}
