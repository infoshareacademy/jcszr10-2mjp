using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
