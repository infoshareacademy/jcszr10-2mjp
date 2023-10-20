using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Pole email nie może być puste")]
        [EmailAddress(ErrorMessage = "Błędny email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole hasło nie może być puste")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
