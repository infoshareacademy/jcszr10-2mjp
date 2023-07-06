
namespace VacationCalendar.BusinessLogic.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<VacationRequest> vRequests { get; set; } = new List<VacationRequest>() { };
    }
}
