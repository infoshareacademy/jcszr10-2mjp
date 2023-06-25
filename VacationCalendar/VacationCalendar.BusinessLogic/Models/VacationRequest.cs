using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Models
{
    internal class VacationRequest
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int  Days 
        { 
            set { } 
        }
    }
}
