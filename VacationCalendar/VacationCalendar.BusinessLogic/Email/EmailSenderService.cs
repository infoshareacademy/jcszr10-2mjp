using System.Net;
using System.Net.Mail;

namespace VacationCalendar.BusinessLogic.Email
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string reciver, string employeeEmail)
        {
            string mail = File.ReadAllText("C:\\VacationCalendarEmail\\email.txt");
            string pwd = File.ReadAllText("C:\\VacationCalendarEmail\\pwd.txt");        

            var client = new SmtpClient("smtp.mail.yahoo.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pwd)
            };
            var subject = "Nowy wniosek urlopowy";
            var message = $"Pracownik {employeeEmail} wygenerował nowy wniosek urlopowy.";
            return client.SendMailAsync(
                new MailMessage(from: mail, to: reciver, subject, message));
        }
    }
}
