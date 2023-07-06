using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Data
{ 
    public class Employees
    {
        public List<Employee> EmployeesList { get; set; } = new List<Employee>
            {
                new Employee()
                {
                    Id = 1,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Anna",
                    LastName = "Malinowaka"
                },
                new Employee()
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Doe"
                }
            };
    }
}
