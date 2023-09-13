using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface ICountVacationDaysLogic
    {
        public int CountVacationDays(DateTime dateFrom, DateTime dateTo, out string message);
        public int CountVacationDays(DateTime dateFrom, DateTime dateTo);
    }
}
