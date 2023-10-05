using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VacationCalendar.BusinessLogic.Dtos;
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
        [HttpPost]
        public async Task<IActionResult> EditSettings(int vacationDays, int roleId)
        {
            await _adminService.EditSettings(vacationDays, roleId);
            return RedirectToAction("Register", "Account");

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
        [HttpGet]
        public async Task<IActionResult> EditEmployee(Guid id) 
        {
            var emplooyee = await _adminService.GetEmployeeDtoAsync(id);
            ViewBag.RoleId = new SelectList(await _adminService.GetRolesAsync(), "Id", "Name");
            return View(emplooyee);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditEmployeeDto dto)
        {
            await _adminService.EditEmployeeAsync(dto);
            return RedirectToAction("GetEmployees");
        }

    }
}
