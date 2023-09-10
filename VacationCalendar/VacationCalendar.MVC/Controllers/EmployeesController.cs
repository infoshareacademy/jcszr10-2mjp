using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;

namespace VacationCalendar.MVC.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
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

        [HttpPost]
        public IActionResult VacationRequestDelete(int id)
        {
            _employeeService.DeleteVacationRequest(id);
            return RedirectToAction("VacationRequests"); 
        }
    }
}
