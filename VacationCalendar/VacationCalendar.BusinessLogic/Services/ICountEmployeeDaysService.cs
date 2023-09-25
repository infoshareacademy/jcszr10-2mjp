using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface ICountEmployeeDaysService
    {
        public Task<int?> CountEmployeeDays(List<VacationRequest> vacationRequests, string email);
    
    }
}
