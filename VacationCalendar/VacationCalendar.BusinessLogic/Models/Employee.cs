
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace VacationCalendar.BusinessLogic.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Pole FirstName jest wymagane")]
        public string? FirstName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Pole LastName jest wymagane")]
        public string? LastName { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Pole Email jest wymagane")]
        public string Email { get; set; }
        public string? PasswordHash { get; set; }
        public bool FirstPasswordChange { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }

        [Display(Name = "Przyznane dni urlopu")]
        public int? VacationDays { get; set; }
    }
}
