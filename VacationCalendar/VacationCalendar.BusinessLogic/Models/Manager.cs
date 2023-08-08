
namespace VacationCalendar.BusinessLogic.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>() { };
    }
}
