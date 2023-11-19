using System.ComponentModel.DataAnnotations;

namespace VacationCalendar.BusinessLogic.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Display(Name = "Rola pracownika")]
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
