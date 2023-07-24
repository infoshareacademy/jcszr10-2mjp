
using Newtonsoft.Json;
using System.IO;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public static class ManagerService
    {
        static string pathToManagers = $@"{DirectoryPathProvider.GetSolutionDirectoryInfo().FullName}\VacationCalendar.BusinessLogic\Data\Managers.json";
        private static List<Manager> DeserializeManagers()
        {
            var managersSerialized = File.ReadAllText(pathToManagers);
            List<Manager> managers = JsonConvert.DeserializeObject<List<Manager>>(managersSerialized);

            return managers;
        }
     
        public static Manager LogInManager(string firstname, string lastname)
        {
            var managers = DeserializeManagers();

            var manager = managers
                .FirstOrDefault(e => e
                .FirstName.ToLower() == firstname.ToLower() && e.LastName.ToLower() == lastname.ToLower());

            return manager;

        }
        public static void GetManagersToString()
        {
            var managers = DeserializeManagers();

            foreach (var manager in managers)
            {
                Console.WriteLine($"{manager.Id} {manager.FirstName} {manager.LastName}");
            }
        }

        public static List<Manager> GetManagers()
        {
            return DeserializeManagers();
        }
    }
}
