using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Models
{
    internal class Manager : Employee
    {
        public void ConformVacation(VacationRequest vacationRequest)
        {
            vacationRequest.Confirmed = true;
        }
    }
}
