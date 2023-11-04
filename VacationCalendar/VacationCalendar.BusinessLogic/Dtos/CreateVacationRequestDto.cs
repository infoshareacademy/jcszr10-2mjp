using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VacationCalendar.BusinessLogic.Dtos
{
    public class CreateVacationRequestDto
    {
        [DisplayName("Data od")]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DisplayName("Data do")]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        public string Email { get; set; }
    }
}
