using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IAdminService
    {
        public Task <List<VacationRequest>> GetVacationRequestsAsync();
        public Task<List<Employee>> GetEmployeesAsync();
        public Task DeleteVacationRequestAsync(int id);
        public Task DeleteEmployeeAsync(Guid id);
    }
}
