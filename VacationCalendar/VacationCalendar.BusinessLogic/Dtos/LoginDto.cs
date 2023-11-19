using System.ComponentModel.DataAnnotations;

namespace VacationCalendar.BusinessLogic.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Pole email nie może być puste")]
        [EmailAddress(ErrorMessage = "Błędny email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole hasło nie może być puste")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj")]
        public bool RememberMe { get; set; }
    }
}
