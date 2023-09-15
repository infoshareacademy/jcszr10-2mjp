using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Dtos
{
    public class EditVacationRequestDto
    {
        public int Id { get; set; }

        [DisplayName("Data od")]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DisplayName("Data do")]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
    }
}
