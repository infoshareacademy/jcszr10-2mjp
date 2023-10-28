using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Data;
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
            var employee = await _adminService.GetEmployeeDtoAsync(id);
            var currentManager = await _adminService.GetEmployeeByIdAsync(employee.ManagerId);

            if (currentManager != null)
            {
                ViewBag.CurrentManager = currentManager.LastName;
            }
            else
            {
                ViewBag.CurrentManager = null;
            }
            ViewBag.RoleId = new SelectList(await _adminService.GetRolesAsync(), "Id", "Name");
            ViewBag.Managers = new SelectList(await _adminService.GetManagersAsync(), "Id", "LastName");
           
            ViewData["employeeEmail"] = employee.Email;
            return View(employee);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditEmployeeDto dto)
        {
            var emplooyee = await _adminService.GetEmployeeDtoAsync(dto.Id);
            if (!ModelState.IsValid)
            {
                ViewBag.RoleId = new SelectList(await _adminService.GetRolesAsync(), "Id", "Name");
                ViewBag.Managers = new SelectList(await _adminService.GetManagersAsync(), "Id", "LastName");
                return View(dto);
            }
            ViewBag.RoleId = new SelectList(await _adminService.GetRolesAsync(), "Id", "Name");
            ViewBag.Managers = new SelectList(await _adminService.GetManagersAsync(), "Id", "LastName");
            await _adminService.EditEmployeeAsync(dto);

            ViewData["employeeEmail"] = emplooyee.Email;
            return View(dto);
        }
    }
}
