using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IAccountService 
    {
        public Task RegisterEmployee(RegisterEmployeeDto dto);
        public Task<Employee> GetEmployeeByEmail(string email);
        public Task<List<Role>> GetRolesAsync();
        public Task ChangePassword(ChangePasswordDto dto, Employee emp);
        public Task LoginAsync(LoginDto dto, Employee employee);
    }
}
