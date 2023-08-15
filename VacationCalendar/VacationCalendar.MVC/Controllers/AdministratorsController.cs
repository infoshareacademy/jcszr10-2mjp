using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    public class AdministratorsController : Controller
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorsController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        public IActionResult Index(string login, string password)
        {
            var isLogIn = _administratorService.LogIn(login, password);
            if (isLogIn)
            {
                return View();
            }
            TempData["LoginAsAdmin"] = "Niepoprawne logowanie administratora";
            return RedirectToAction("Index", "Home");
        }
    }
}
