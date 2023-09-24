using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationCalendar.BusinessLogic.Models;
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
        public async Task<IActionResult> GetVacationRequests()
        {
            var requests = await _adminService.GetVacationRequestsAsync();
            return View("GetVacationRequests", requests);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVacationRequest(int id)
        {
            await _adminService.DeleteVacationRequestAsync(id);
            TempData["DeleteConfirmed"] = "Wniosek został usunięty";
            return RedirectToAction("GetVacationRequests");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _adminService.GetEmployeesAsync();
            return View(employees);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _adminService.DeleteEmployeeAsync(id);
            TempData["DeleteConfirmed"] = "Pracownik został usunięty";
            return RedirectToAction("GetEmployees");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

    }
}
