using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Models
{
    public class VacationRequest
    {
        private static int nextID = 0;
        public int ID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public VacationRequest(DateTime from, DateTime to)
        {
            ID = nextID;
            nextID++;
            From = from;
            To = to;
        }
    
    }
}
