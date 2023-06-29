using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Models
{
    public class VacationRequest
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public TimeSpan NumberOfDays 
        {
            get { return To.Subtract(From); }
        } 
    }
}
