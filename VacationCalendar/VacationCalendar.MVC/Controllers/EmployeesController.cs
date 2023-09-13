using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICountVacationDaysLogic _countVacationDaysLogic;
        public EmployeesController(IEmployeeService employeeService, ICountVacationDaysLogic countVacationDaysLogic)
        {
            _employeeService = employeeService;
            _countVacationDaysLogic = countVacationDaysLogic;
        }

        // GET: EmployeesController
        public ActionResult GetEmployees()
        {
            var employees = _employeeService.GetAll();
            return View(employees);
        }
        public ActionResult Index()
        {
            return View();
        }


        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> VacationRequests()
        {
            var requests = await _employeeService.GetVacationRequests(User.Identity.Name);
            return View(requests);
        }

        public async Task<IActionResult> DeleteVacationRequest(int id)
        {
            await _employeeService.DeleteVacationRequest(id);
            TempData["DeleteConfirmed"] = "Wniosek został usunięty";
            return RedirectToAction("VacationRequests"); 
        }
        [HttpGet]
        public async Task<IActionResult> EditVacationRequest(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _employeeService.GetVacationRequest(id);

            if (vacationRequest == null)
            {
                return NotFound();
            }

            var dto = new EditVacationRequestDto()
            {
                From = vacationRequest.From,
                To = vacationRequest.To    
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditVacationRequest(EditVacationRequestDto dto)
        {
            var vacationRequest = await _employeeService.GetVacationRequest(dto.Id);
            if (vacationRequest.RequestStatusId != 1)
            {
                TempData["EditRequestMessage"] = "Można edytować wniosek, który jeszcze nie został zatwirdzony lub odrzucony.";
                TempData["message-type"] = "warning";
                return RedirectToAction(nameof(VacationRequests));
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(VacationRequests));
                }
                string message = "";

                var days = _countVacationDaysLogic.CountVacationDays(dto.From, dto.To, out message);

                if (days == 0)
                {
                    TempData["EditRequestMessage"] = $"{message}";
                    TempData["message-type"] = "warning";
                }
                if (days > 0)
                {
                    TempData["EditRequestMessage"] = $"{message} {days}";
                    TempData["message-type"] = "success";
                }

                await _employeeService.EditVacationRequest(dto);

                return RedirectToAction(nameof(VacationRequests));
            }
            catch (Exception e)
            {
                TempData["EditRequestMessage"] = $"{e.Message}";
                TempData["message-type"] = "danger";
                return View();
            }
        }
    }
}
