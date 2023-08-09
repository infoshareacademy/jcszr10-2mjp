
using System.ComponentModel.DataAnnotations;

namespace VacationCalendar.BusinessLogic.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int? ManagerId { get; set; }
        public Manager? Manager { get; set; }
        public List<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>() { };
    }
}
