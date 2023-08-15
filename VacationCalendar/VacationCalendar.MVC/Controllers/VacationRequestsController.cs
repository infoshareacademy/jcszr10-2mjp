using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VacationCalendar.MVC.Controllers
{
    public class VacationRequestsController : Controller
    {
        // GET: VacationRequestsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VacationRequestsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VacationRequestsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VacationRequestsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
