using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Email
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string reciver, string employeeEmail);
    }
}
