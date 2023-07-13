
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public static class ManagerService
    {
        public static Manager LogInManager(string firstname, string lastname, Managers managers)
        {
            var allManagers = managers.ManagersList;
            var manager = allManagers
                .FirstOrDefault(e => e
                .FirstName.ToLower() == firstname.ToLower() && e.LastName.ToLower() == lastname.ToLower());

            return manager;
        }
        public static void GetManagers(Managers managers)
        {
            foreach (var manager in managers.ManagersList)
            {
                Console.WriteLine($"{manager.Id} {manager.FirstName} {manager.LastName}");
            }
        }
    }
}
