using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    public class AccountController : Controller
    {
        public readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService; 
        }

        public IActionResult Register()
        {
            ViewBag.RoleId = new SelectList(_accountService.GetDbContext().Roles, "Id", "Name");
            return View("Register");
        }
        [HttpPost]
        public ActionResult Register(RegisterEmployeeDto dto)
        {
            _accountService.RegisterEmployee(dto);
           return RedirectToAction("GetEmployees", "Employees");
        }
    }
}
