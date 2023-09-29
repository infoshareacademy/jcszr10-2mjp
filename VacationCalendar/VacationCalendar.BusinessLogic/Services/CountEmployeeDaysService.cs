using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{

    public class CountEmployeeDaysService : ICountEmployeeDaysService
    {
        private readonly VacationCalendarDbContext _context;
       

        public CountEmployeeDaysService(VacationCalendarDbContext context)
        {
            _context = context;
        }
        public async Task<int?> CountEmployeeDays(List<VacationRequest> vacationRequests, string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            int? employeeVacationDays = employee.VacationDays;
            int? daysCounter = 0;

            foreach (var request in vacationRequests)
            {  
                if(request.RequestStatus.Id != 3)
                    daysCounter += request.VacationDays;
            }
            int? freeDays = employeeVacationDays - daysCounter;
    
            return freeDays;
        }  
    }
}
