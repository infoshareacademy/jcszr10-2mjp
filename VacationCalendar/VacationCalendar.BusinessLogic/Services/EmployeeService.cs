using Newtonsoft.Json;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class EmployeeService
    {
        static string path = $@"{DirectoryPathProvider.GetSolutionDirectoryInfo().FullName}\VacationCalendar.BusinessLogic\Data\Employees.json";
        private static List<Employee> DeserializeEmployee()
        {
            var employeeSerialized = File.ReadAllText(path);
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(employeeSerialized);
            return employees;
        }
        public static Employee LogInEmployee(string firstname, string lastname)
        {
            var employees = DeserializeEmployee();

            var employee = employees
                .FirstOrDefault(e => e
                .FirstName.ToLower() == firstname.ToLower() && e.LastName.ToLower() == lastname.ToLower());

            return employee;
        }
        public static void GetEmployeesToString()
        {
            var employees = DeserializeEmployee();

            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}; {employee.FirstName} {employee.LastName}; Id menadżera: {employee.ManagerId}");            
            }
        }
        public static List<Employee> GetEmployees()
        {
            return DeserializeEmployee();
        }
    }
}
