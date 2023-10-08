using System.ComponentModel.DataAnnotations;

namespace VacationCalendar.BusinessLogic.Models
{
    public class AdminSettings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DefaultVacationDays { get; set; } = 26;

        public Role DefaultEmployeeRole { get; set; }

        public int RoleId { get; set; } = 3;
    }
}
