using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var requests = await _adminService.GetVacationRequestsAsync();
            return View(requests);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.Delete(id);
            TempData["DeleteConfirmed"] = "Wniosek został usunięty";
            return RedirectToAction("Index");
        }
    }
}
