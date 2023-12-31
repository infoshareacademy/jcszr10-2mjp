﻿using System.ComponentModel.DataAnnotations;

namespace VacationCalendar.BusinessLogic.Dtos
{
    public class RegisterEmployeeDto
    {
        [Display(Name = "Imię")]
        public string? FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = $"Pole \"Email\" jest wymagane")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = $"Pole \"Hasło\" jest wymagane")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        [Required(ErrorMessage = "Pole \"Potwierdź hasło\" jest wymagane")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Rola pracownika")]
        public int RoleId { get; set; } = 3;

        [Display(Name = "Przyznane dni urlopu")]
        public int VacationDays { get; set; }

        [Display(Name = "Manager")]
        public Guid ManagerId { get; set; }
    }
}
