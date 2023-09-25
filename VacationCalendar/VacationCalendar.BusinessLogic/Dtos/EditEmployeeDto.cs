using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Dtos
{
    public class EditEmployeeDto
    {
        public Guid Id { get; set; }

        [Display(Name = "Imię")]
        public string? FirstName { get; set; }

        [Display(Name = "Nazwiasko")]
        public string? LastName { get; set; }
        public string Email { get; set; }

        [Display(Name = "Przyznane dni urlopu")]
        public int VacaationDays { get; set; }

        [Display(Name = "Rola pracownika")]
        public int RoleId { get; set; } = 3;
    }
}
