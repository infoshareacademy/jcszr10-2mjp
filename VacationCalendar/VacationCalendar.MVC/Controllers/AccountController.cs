using Microsoft.AspNetCore.Mvc;
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
