
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VacationCalendar.BusinessLogic.Models
{
    public class Manager
    {
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>() { };
    }
}
