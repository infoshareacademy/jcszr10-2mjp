using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Dtos
{
    public class RegisterEmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }
        public int RoleId { get; set; } = 3;
    }
}
