using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }
      
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _dbContext.Employees.Include(e => e.Role).FirstOrDefaultAsync(e => e.Email == email);
        } 
        public async Task RegisterEmployee(RegisterEmployeeDto dto) 
        {
            var newEmployee= new Models.Employee()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = dto.RoleId,
                VacationDays = dto.VacationDays
            };
            var hashedPassword = _passwordHasher.HashPassword(newEmployee, dto.Password);
            newEmployee.PasswordHash = hashedPassword;
            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
        }
    }
}
