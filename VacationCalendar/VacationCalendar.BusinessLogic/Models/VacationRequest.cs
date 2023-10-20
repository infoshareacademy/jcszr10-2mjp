using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VacationCalendar.BusinessLogic.Models
{
    public class VacationRequest
    {
        public int Id { get; set; }

        [DisplayName("Data od")]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DisplayName("Data do")]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int RequestStatusId { get; set; } = 1;

        [DisplayName("Status wniosku")]
        public RequestStatus RequestStatus { get; set; }

        [DisplayName("Dni urlopu")]
        public int? VacationDays { get; set; }

        [DisplayName("Uzasadnienie")]
        [StringLength(250, ErrorMessage = "Uzasadnienie może zawierać maksymalnie {1} znaków.")]
        public string? Message { get; set; }
    }
}
