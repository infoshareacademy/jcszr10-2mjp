using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace VacationCalendar.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly VacationCalendarDbContext _dbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly IToastNotification _toastNotification;

        public AccountService(VacationCalendarDbContext dbContext, IPasswordHasher<Employee> passwordHasher, IToastNotification toastNotification)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _toastNotification = toastNotification;
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

        public async Task ChangePassword(ChangePasswordDto dto, Employee emp) 
        {
            if (!emp.FirstPasswordChange)
            {
                emp.FirstPasswordChange = true;
            }

            var hashedNewPassword = _passwordHasher.HashPassword(emp, dto.NewPassword);
            emp.PasswordHash = hashedNewPassword;
            await _dbContext.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("Udana zmiana hasła");
        }

    }
}
