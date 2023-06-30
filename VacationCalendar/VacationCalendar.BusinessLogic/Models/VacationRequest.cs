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
            get 
            {  
                return To.Subtract(From); 
            }
        }
        /// <summary>
        /// Metoda oblicza dni wakacji, pomija soboty i niedziele
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int CountVacationDays(string from, string to, out string message)
        {
            DateTime dateValueFrom;
            string dateStringFrom = from;
            DateTime.TryParse(dateStringFrom, out dateValueFrom);
            From = dateValueFrom;

            DateTime dateValueTo;
            string dateStringTo = to;
            DateTime.TryParse(dateStringTo, out dateValueTo);
            To = dateValueTo;

            if (dateValueFrom >= dateValueTo)
            {
                message = "\"Data od\" nie może być nowsza od \"daty do\"! Dni urlopu:";
                return 0;
            }
            var numberOfDays = NumberOfDays.Days;

            List<DateTime> allDays = new List<DateTime>
            {
                dateValueFrom
            };

            for (int i = 1; i <= numberOfDays; i++)
            {
                DateTime tmpDate;
                tmpDate = dateValueFrom.AddDays(i);
                allDays.Add(tmpDate);
            }

            List<DateTime> daysWithoutWeekend = new List<DateTime>();

            foreach (DateTime date in allDays)
            {
                var dayOfWeek = date.DayOfWeek;
                if (dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Saturday)
                {
                    continue;
                }
                daysWithoutWeekend.Add(date);
            }
            message = "Dni urlopu:";
            return daysWithoutWeekend.Count;
        }
    }
}
