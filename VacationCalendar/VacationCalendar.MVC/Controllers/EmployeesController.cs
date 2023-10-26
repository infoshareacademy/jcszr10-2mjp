using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICountVacationDaysService _countVacationDaysService;
        private readonly ICountEmployeeDaysService _countEmployeeDaysService;
        public EmployeesController(IEmployeeService employeeService, ICountVacationDaysService countVacationDaysService, ICountEmployeeDaysService countEmployeeDaysService)
        {
            _employeeService = employeeService;
            _countVacationDaysService = countVacationDaysService;
            _countEmployeeDaysService = countEmployeeDaysService;
        }

        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> CreateVacationRequest()
        {
            return View(nameof(CreateVacationRequest));
        }

        // POST: VacationRequestsController/Create
        [Authorize(Roles = "employee,manager")]
        [HttpPost]
        public async Task<IActionResult> CreateVacationRequest(CreateVacationRequestDto dto)
        {
            var vacationRequests = await _employeeService.GetVacationRequests(dto.Email);

            int? freeDays = await _countEmployeeDaysService.CountEmployeeDays(vacationRequests, dto.Email);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var days = _countVacationDaysService.VacationDaysValidation(dto.From, dto.To);


            bool isPreviusRequestContainsCurrentRequest = _countVacationDaysService.IsPreviusRequestContainsCurrentRequest(dto, vacationRequests);
            if (!isPreviusRequestContainsCurrentRequest) return View();

            var daysAfterRequest = freeDays - days;

            if (daysAfterRequest < 0)
            {
                TempData["RequestMessage"] = "Twój wniosek przekracza ilość dni urlopu do wykorzystanaia.";
                TempData["message-type"] = "danger";
                return View();
            }

            //if (days == 0)
            //{
            //    TempData["RequestMessage"] = $"{message}";
            //    TempData["message-type"] = "warning";
            //}
            //if (days > 0)
            //{
            //    TempData["RequestMessage"] = $"{message} {days}";
            //    TempData["message-type"] = "success";
            //}

            await _employeeService.CreateVacationRequest(dto);

            return RedirectToAction(nameof(CreateVacationRequest));

        }

        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> GetVacationRequests()
        {
            var requests = await _employeeService.GetVacationRequests(User.Identity.Name);
            int? freeDays = await _countEmployeeDaysService.CountEmployeeDays(requests, User.Identity.Name);
            if (freeDays != null)
            {
                if (freeDays < 0)
                {
                    ViewData["FreeDays"] = 0;
                }
                ViewData["FreeDays"] = freeDays;
            }
            return View(requests);
        }

        [Authorize(Roles = "employee")]
        public async Task<IActionResult> DeleteVacationRequest(int id)
        {
            await _employeeService.DeleteVacationRequest(id);
            TempData["DeleteConfirmed"] = "Wniosek został usunięty";
            return RedirectToAction("GetVacationRequests");
        }
        [HttpGet]
        [Authorize(Roles = "employee,manager")]
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
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> EditVacationRequest(EditVacationRequestDto dto)
        {
            var vacationRequest = await _employeeService.GetVacationRequest(dto.Id);
            if (vacationRequest.RequestStatusId != 1)
            {
                TempData["EditRequestMessage"] = "Można edytować wniosek, który jeszcze nie został zatwirdzony lub odrzucony.";
                TempData["message-type"] = "warning";
                return RedirectToAction(nameof(GetVacationRequests));
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(GetVacationRequests));
                }


                var days = _countVacationDaysService.VacationDaysValidation(dto.From, dto.To);

                //if (days == 0)
                //{
                //    TempData["EditRequestMessage"] = $"{message}";
                //    TempData["message-type"] = "warning";
                //}
                //if (days > 0)
                //{
                //    TempData["EditRequestMessage"] = $"{message} {days}";
                //    TempData["message-type"] = "success";
                //}

                await _employeeService.EditVacationRequest(dto);

                return RedirectToAction(nameof(GetVacationRequests));
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
