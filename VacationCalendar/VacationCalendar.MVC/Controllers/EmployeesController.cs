﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ICountVacationDaysService _countVacationDaysService;
        public EmployeesController(IEmployeeService employeeService, ICountVacationDaysService countVacationDaysService)
        {
            _employeeService = employeeService;
            _countVacationDaysService = countVacationDaysService;
        }

        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> CreateVacationRequest()
        {
            return View(nameof(CreateVacationRequest));
        }

        // POST: VacationRequestsController/Create
        [HttpPost]
        public async Task<IActionResult> CreateVacationRequest(CreateVacationRequestDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                string message = "";

                var days = _countVacationDaysService.CountVacationDays(dto.From, dto.To, out message);

                if (days == 0)
                {
                    TempData["RequestMessage"] = $"{message}";
                    TempData["message-type"] = "warning";
                }
                if (days > 0)
                {
                    TempData["RequestMessage"] = $"{message} {days}";
                    TempData["message-type"] = "success";
                }

                await _employeeService.CreateVacationRequest(dto);

                return RedirectToAction(nameof(CreateVacationRequest));
            }
            catch (Exception e)
            {
                TempData["RequestMessage"] = $"{e.Message}";
                TempData["message-type"] = "danger";
                return View();
            }
        }

        public async Task<IActionResult> GetVacationRequests()
        {
            var requests = await _employeeService.GetVacationRequests(User.Identity.Name);
            return View(requests);
        }

        [Authorize(Roles = "employee,manager")]
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
                string message = "";

                var days = _countVacationDaysService.CountVacationDays(dto.From, dto.To, out message);

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
