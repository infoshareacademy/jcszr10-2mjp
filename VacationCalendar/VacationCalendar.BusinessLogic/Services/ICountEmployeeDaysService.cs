using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface ICountEmployeeDaysService
    {
        public Task<int?> CountEmployeeDays(List<VacationRequest> vacationRequests, string email);
    
    }
}
