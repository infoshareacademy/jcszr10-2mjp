
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationCalendar.BusinessLogic.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int? ManagerId { get; set; }
        public Manager? Manager { get; set; }
        public List<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>() { };
    }
}
