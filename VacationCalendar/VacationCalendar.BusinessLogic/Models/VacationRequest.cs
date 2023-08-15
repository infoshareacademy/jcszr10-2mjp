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
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int RequestStatusId { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public int NumberOfDays { get; set; }
    }
}
