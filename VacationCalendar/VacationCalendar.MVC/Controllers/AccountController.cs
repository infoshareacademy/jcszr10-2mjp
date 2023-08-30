using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Migrations;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    public class AccountController : Controller
    {
        public readonly IAccountService _accountService;

        private readonly IPasswordHasher<Employee> _passwordHasher;
        public AccountController(IAccountService accountService, IPasswordHasher<Employee> password)
        {
            _accountService = accountService; 
            _passwordHasher = password;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Register()
        {
            ViewBag.RoleId = new SelectList(_accountService.GetDbContext().Roles, "Id", "Name");
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterEmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
               return View(dto);
            }
       
            _accountService.RegisterEmployee(dto);
           return RedirectToAction("GetEmployees", "Employees");
        }
        public ActionResult Login()
        {
            return View(); 
        }


        [HttpPost]
        public async Task<ActionResult> LoginAsync(LoginDto dto)
        {
            var employee = _accountService.GetDbContext().Employees.Include(e => e.Role).FirstOrDefault(e => e.Email == dto.Email);
            if (employee == null)
            {
                return View();
            }
              
            var result = _passwordHasher.VerifyHashedPassword(employee, employee.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"),
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(ClaimTypes.Role, $"{employee.Role.Name}"),
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties()
                {
                    IsPersistent = dto.RememberMe
                };

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);

                return RedirectToAction("Index", "Home");
            }

            TempData["Message"] = $"Login failed";

            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Account");
        }
    }
}
