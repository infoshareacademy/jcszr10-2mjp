namespace VacationCalendar.MVC.Models
{
    public class LanguageSwitcherViewModel
    {
        public string CurrentCulture { get; set; }
        public IEnumerable<string> SupportedCultures { get; set; }
    }
}
