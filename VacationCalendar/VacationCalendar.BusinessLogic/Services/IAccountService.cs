using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IAccountService 
    {
        void RegisterEmployee(RegisterEmployeeDto dto);
        VacationCalendarDbContext GetDbContext();
    }
}
