using Microsoft.AspNetCore.Mvc;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
 
    public class ManagersController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        public async Task<IActionResult> Index()
        {
            var vacatinRequests = await _managerService.GetVacationRequests();
            return View(vacatinRequests);
        }
    }
}
