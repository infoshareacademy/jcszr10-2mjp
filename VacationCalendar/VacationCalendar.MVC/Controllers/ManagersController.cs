using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    [Authorize]
    public class ManagersController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Index()
        {
            var vacatinRequests = await _managerService.GetVacationRequests();
            return View(vacatinRequests);
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Accept(int id)
        {
            var vacationRequest = await _managerService.GetVacationRequestById(id);
            _managerService.Accept(vacationRequest);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Reject(int id)
        {
            var vacationRequest = await _managerService.GetVacationRequestById(id);
            _managerService.Reject(vacationRequest);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Delete(int id)
        {
            await _managerService.Delete(id);
            TempData["DeleteConfirmed"] = "Wniosek został usunięty";
            return RedirectToAction("Index");
        }
    }
}
