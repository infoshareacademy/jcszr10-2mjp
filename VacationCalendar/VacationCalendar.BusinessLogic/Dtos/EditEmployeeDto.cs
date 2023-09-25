using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Dtos
{
    public class EditEmployeeDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public int VacaationDays { get; set; }
        public int RoleId { get; set; } = 3;
    }
}
