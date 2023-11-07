using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Email;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICountVacationDaysService _countVacationDaysService;
        private readonly ICountEmployeeDaysService _countEmployeeDaysService;
        private readonly IEmailSender _emailSender;
        public EmployeesController(IEmployeeService employeeService, ICountVacationDaysService countVacationDaysService, ICountEmployeeDaysService countEmployeeDaysService, IEmailSender emailSender)
        {
            _employeeService = employeeService;
            _countVacationDaysService = countVacationDaysService;
            _countEmployeeDaysService = countEmployeeDaysService;
            _emailSender = emailSender;
        }

        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> CreateVacationRequest()
        {
            var requests = await _employeeService.GetVacationRequests(User.Identity.Name);
            int? freeDays = await _countEmployeeDaysService.CountEmployeeDays(requests, User.Identity.Name);
            if (freeDays != null)
            {
                if (freeDays < 0)
                {
                    TempData["VacationDays"] = 0;
                }
                TempData["VacationDays"] = freeDays;
            }
            return View(nameof(CreateVacationRequest));
        }

        // POST: VacationRequestsController/Create
        [Authorize(Roles = "employee,manager")]
        [HttpPost]
        public async Task<IActionResult> CreateVacationRequest(CreateVacationRequestDto dto)
        {
            var requests = await _employeeService.GetVacationRequests(User.Identity.Name);
            int? freeDays = await _countEmployeeDaysService.CountEmployeeDays(requests, User.Identity.Name);
            if (freeDays != null)
            {
                if (freeDays < 0)
                {
                    TempData["VacationDays"] = 0;
                }
                TempData["VacationDays"] = freeDays;
            }
            var vacationRequests = await _employeeService.GetVacationRequests(dto.Email);

            int? vacationDays = await _countEmployeeDaysService.CountEmployeeDays(vacationRequests, dto.Email);

            if (!ModelState.IsValid)
            {
                return View();
            }
            var requestDays = _countVacationDaysService.CountVacationDays(dto.From, dto.To);

            bool isPreviusRequestContainsCurrentRequest = _countVacationDaysService.IsPreviusRequestContainsCurrentRequest(dto, vacationRequests);
            if (isPreviusRequestContainsCurrentRequest) return View();

            bool isVacationDaysAfterRequest = _countVacationDaysService.IsVacationDaysAfterRequest(vacationDays, requestDays);
            if (!isVacationDaysAfterRequest) return View();

            _countVacationDaysService.VacationDaysValidation(dto.From, dto.To);
            await _employeeService.CreateVacationRequest(dto);

            var reciver = "pit.pit@wp.pl";
            var subject = "Nowy wniosek";
            var message = $"Pracownik {User.Identity.Name} wygenerował nowy wniosek urlopowy.";
    
            _emailSender.SendEmailAsync(reciver, subject, message);

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
                    TempData["FreeDays"] = 0;
                }
                TempData["FreeDays"] = freeDays;
            }
            return View(requests);
        }

        [Authorize(Roles = "employee")]
        public async Task<IActionResult> DeleteVacationRequest(int id)
        {
            await _employeeService.DeleteVacationRequest(id);
            return RedirectToAction("GetVacationRequests");
        }
        [HttpGet]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> EditVacationRequest(int id, string email)
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
        public async Task<IActionResult> EditVacationRequest(EditVacationRequestDto dto, string email)
        {
            var vacationRequest = await _employeeService.GetVacationRequest(dto.Id);
            var vacationRequests= await _employeeService.GetVacationRequests(email);
            if (vacationRequest.RequestStatusId != 1)
            {
                TempData["EditRequestMessage"] = "Można edytować wniosek, który jeszcze nie został zatwirdzony lub odrzucony.";
                TempData["message-type"] = "warning";
                return RedirectToAction(nameof(GetVacationRequests));
            }
            int? vacationDays = await _countEmployeeDaysService.CountEmployeeDays(vacationRequests, email);
            var requestDays = _countVacationDaysService.CountVacationDays(dto.From, dto.To);

            int holidayDaysRefunded = (int)vacationDays + (int)vacationRequest.VacationDays;

            bool isVacationDaysAfterRequest = _countVacationDaysService.IsVacationDaysAfterRequest(holidayDaysRefunded, requestDays);
            if (!isVacationDaysAfterRequest) return RedirectToAction(nameof(GetVacationRequests));

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(GetVacationRequests));
            }

            var days = _countVacationDaysService.VacationDaysValidation(dto.From, dto.To);

            await _employeeService.EditVacationRequest(dto);

            return RedirectToAction(nameof(GetVacationRequests));
        }
    }
}
