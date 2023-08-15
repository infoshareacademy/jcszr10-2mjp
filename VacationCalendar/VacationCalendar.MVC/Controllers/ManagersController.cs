using Microsoft.AspNetCore.Mvc;

namespace VacationCalendar.MVC.Controllers
{
    public class ManagersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
