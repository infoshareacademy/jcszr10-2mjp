using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;
using Microsoft.AspNetCore.Identity;

namespace VacationCalendar.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly VacationCalendarDbContext _dbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public AccountService(VacationCalendarDbContext dbContext, IPasswordHasher<Employee> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public VacationCalendarDbContext GetDbContext()
        {
            return _dbContext;
        }
        public void RegisterEmployee(RegisterEmployeeDto dto) 
        {
            var newEmployee= new Models.Employee()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = dto.RoleId,
            };
            var hashedPassword = _passwordHasher.HashPassword(newEmployee, dto.Password);
            newEmployee.PasswordHash = hashedPassword;
            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
        }
    }
}
