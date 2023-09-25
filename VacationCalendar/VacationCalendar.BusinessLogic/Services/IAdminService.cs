using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IAdminService
    {
        public Task<List<Role>> GetRolesAsync();
        public Task <List<VacationRequest>> GetVacationRequestsAsync();
        public Task<List<Employee>> GetEmployeesAsync();
        public Task DeleteVacationRequestAsync(int id);
        public Task DeleteEmployeeAsync(Guid id);
        public Task<EditEmployeeDto> GetEmployeeDtoAsync(Guid id);
        public Task EditEmployeeAsync(EditEmployeeDto dto);
    }
}
