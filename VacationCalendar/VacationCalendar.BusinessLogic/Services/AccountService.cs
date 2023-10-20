using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace VacationCalendar.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly VacationCalendarDbContext _dbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly IToastNotification _toastNotification;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(VacationCalendarDbContext dbContext, IPasswordHasher<Employee> passwordHasher, IToastNotification toastNotification, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _toastNotification = toastNotification;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LoginAsync(LoginDto dto, Employee employee)
        {
            var result = _passwordHasher.VerifyHashedPassword(employee, employee.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.Email),
                    new Claim(ClaimTypes.Role, $"{employee.Role.Name}"),
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties()
                {
                    IsPersistent = dto.RememberMe
                };

                // albo stworzyc obiekt ktory bedzie miał Claimy i AuthenticationProperties i przekazac do kontrolera i wykonac reszte kodu tam
                await _httpContextAccessor.HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);
                _toastNotification.AddSuccessToastMessage("Zalogowano");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Nie udało się zalogować");
            }
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
