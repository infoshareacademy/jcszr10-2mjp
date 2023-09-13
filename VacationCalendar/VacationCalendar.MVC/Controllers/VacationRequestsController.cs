using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    public class VacationRequestsController : Controller
    {
        private readonly IVacationService _vacationService;

        public VacationRequestsController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }

        // GET: VacationRequestsController/Create

        [Authorize(Roles = "employee,manager")]
        public async Task <IActionResult> Create()
        {
            return View();
        }

        // POST: VacationRequestsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateVacationRequestDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                string message = "";

                var days = _vacationService.CountVacationDays(dto.From, dto.To, out message);

                if (days == 0)
                {
                    TempData["RequestMessage"] = $"{message}";
                    TempData["message-type"] = "warning";
                }
                if(days > 0) 
                {
                    TempData["RequestMessage"] = $"{message} {days}";
                    TempData["message-type"] = "success";
                }

                await _vacationService.CreateVacationRequest(dto);

                return RedirectToAction(nameof(Create));
            }
            catch(Exception e)
            {
                TempData["RequestMessage"] = $"{e.Message}"; 
                TempData["message-type"] = "danger";
                return View();
            }
        }

        // GET: VacationRequestsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VacationRequestsController/Edit/5
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

        // GET: VacationRequestsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VacationRequestsController/Delete/5
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
    }
}
