using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using System.Security.Claims;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly VacationCalendarDbContext _dbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly IToastNotification _toastNotification;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AccountService(VacationCalendarDbContext dbContext, IPasswordHasher<Employee> passwordHasher, IToastNotification toastNotification, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _toastNotification = toastNotification;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<bool> LoginAsync(LoginDto dto, Employee employee)
        {
            PasswordVerificationResult result;
            if (employee == null)
            {
                _toastNotification.AddErrorToastMessage("Nie udało się zalogować");
                return false;
            }

            try
            {
                result = _passwordHasher.VerifyHashedPassword(employee, employee.PasswordHash, dto.Password);
            }
            catch (Exception)
            {
                result = PasswordVerificationResult.Failed;
            }

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

                await _httpContextAccessor.HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);
                _toastNotification.AddSuccessToastMessage("Zalogowano");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Nie udało się zalogować");
                return false;
            }
            return true;
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
            var newEmployee = new Models.Employee()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = dto.RoleId,
                VacationDays = dto.VacationDays,
                ManagerId = dto.ManagerId
            };
            var hashedPassword = _passwordHasher.HashPassword(newEmployee, dto.Password);
            newEmployee.PasswordHash = hashedPassword;
            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
        }

        public async Task ChangePassword(ChangePasswordDto dto, Employee emp)
        {
            if (dto.NewPassword.IsNullOrEmpty() || dto.OldPassword.IsNullOrEmpty() || dto.ConfirmPassword.IsNullOrEmpty())
            {
                _toastNotification.AddErrorToastMessage("Nie udana zmiana hasła");
                return;
            }

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
