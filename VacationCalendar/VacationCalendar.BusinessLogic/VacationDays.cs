using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic
{
    /// <summary>
    /// Klasa pomocnicza do obliczania dni wakacji
    /// </summary>
    public static class VacationDays
    {
        /// <summary>
        /// Metoda oblicza dni wakacji, pomija soboty i niedziele
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static int CountVacationDays(string from, string to)
        {
            VacationRequest vacationRequest = new VacationRequest();

            DateTime dateValueFrom;
            string dateStringFrom = from;
            DateTime.TryParse(dateStringFrom, out dateValueFrom);
            vacationRequest.From = dateValueFrom;

            DateTime dateValueTo;
            string dateStringTo = to;
            DateTime.TryParse(dateStringTo, out dateValueTo);
            vacationRequest.To = dateValueTo;

            var numberOfDays = vacationRequest.NumberOfDays.Days;

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

            return daysWithoutWeekend.Count;
        }
    }
}
