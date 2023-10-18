using System.ComponentModel.DataAnnotations;

namespace VacationCalendar.BusinessLogic.Dtos
{
    public class ChangePasswordDto
    {
        // Mozna wyrzucic te data annotations
        [DataType(DataType.Password)]
        [Display(Name = "Obecne hasło")]
        public string? OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        public string? ConfirmPassword { get; set; }
    }
}
