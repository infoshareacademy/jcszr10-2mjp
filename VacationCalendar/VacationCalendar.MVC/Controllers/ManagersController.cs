﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var manager = await _managerService.GetEmployeeByEmail(User.Identity.Name);
            Guid managerId = manager.Id; 
            var vacatinRequests = await _managerService.GetVacationRequestsByManager(managerId);
            return View(vacatinRequests);
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Accept(int id)
        {
            var vacationRequest = await _managerService.GetVacationRequestById(id);
            await _managerService.Accept(vacationRequest);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Reject(int id, string message)
        {
            var vacationRequest = await _managerService.GetVacationRequestById(id);
            await _managerService.Reject(vacationRequest, message);
            return RedirectToAction("Index");
        }
    }
}
