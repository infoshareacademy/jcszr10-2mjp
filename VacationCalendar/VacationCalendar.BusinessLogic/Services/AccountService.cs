using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;

namespace VacationCalendar.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly VacationCalendarDbContext _dbContext;

        public AccountService(VacationCalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void RegisterEmployee(RegisterEmployeeDto dto) 
        {
            var newEmployee= new Models.Employee()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = dto.RoleId,
                PasswordHash = dto.Password
            };

            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
        }
    }
}
