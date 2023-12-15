using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace VacationCalendar.MVC.Controllers
{
    public class CultureController : Controller
    {
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            // Save the selected culture in a cookie or session
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            // Redirect back to the previous page or to a specific page
            return LocalRedirect(returnUrl);
        }
    }
}
