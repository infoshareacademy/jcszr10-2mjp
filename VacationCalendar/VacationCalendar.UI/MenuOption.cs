using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.UI
{
    public class MenuOption
    {
        public string Name { get; set; }
        public Action<VacationRequest> Action { get; set; }
        public MenuOption(string name, Action<VacationRequest> action)
        {
            Name = name;
            Action = action;
        }
    }
}
