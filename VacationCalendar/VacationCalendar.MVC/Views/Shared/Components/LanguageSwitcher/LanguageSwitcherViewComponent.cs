using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using VacationCalendar.MVC.Models;

namespace VacationCalendar.MVC.Views.Shared.Components.LanguageSwitcher
{
    public class LanguageSwitcherViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;

            var model = new LanguageSwitcherViewModel
            {
                CurrentCulture = currentCulture,
                SupportedCultures = new[] { "en-US", "pl-PL" }
            };

            return View("Switcher", model);
        }
    }
}
