using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Services
{
    public class CountVacationDaysLogic : ICountVacationDaysLogic
    {
        /// <summary>
        /// Metoda oblicza dni wakacji, pomija soboty i niedziele
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int CountVacationDays(DateTime dateFrom, DateTime dateTo, out string message)
        {

            if (dateFrom > dateTo)
            {
                message = "\"Data od\" nie może być nowsza od \"daty do\"!";
                return 0;
            }

            if (dateFrom.DayOfWeek == DayOfWeek.Saturday || dateFrom.DayOfWeek == DayOfWeek.Sunday
                || dateTo.DayOfWeek == DayOfWeek.Saturday || dateTo.DayOfWeek == DayOfWeek.Sunday)
            {
                message = $"Wniosek nie może zaczynać i kończyć się na sobocie lub niedzieli.";
                return 0;
            }

            if (dateFrom < DateTime.Now)
            {
                message = "Urlop nie może być planowany wstecz ani w dniu brania urlopu.";
                return 0;
            }

            if (dateFrom > DateTime.Now.AddMonths(12))
            {
                message = "Nie możesz planowac tak daleko w przyszłość.";
                return 0;
            }

            if (dateFrom == dateTo)
            {
                message = $"Wystawiono wniosek. Ilość dni urlopu:";
                return 1;
            }

            TimeSpan dateInterval = dateTo - dateFrom;
            int numberOfDays = dateInterval.Days;
            DateTime currentDay;
            int daysWithoutWeekend = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (dateFrom.AddDays(i));
                if (currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday)
                    continue;

                daysWithoutWeekend++;
            }
            message = $"Wystawiono wniosek. Ilość dni urlopu:";
            return daysWithoutWeekend;
        }

        public int CountVacationDays(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
                return 0;

            if (dateFrom.DayOfWeek == DayOfWeek.Saturday || dateFrom.DayOfWeek == DayOfWeek.Sunday
                || dateTo.DayOfWeek == DayOfWeek.Saturday || dateTo.DayOfWeek == DayOfWeek.Sunday)
            {
                return 0;
            }

            if (dateFrom < DateTime.Now)
                return 0;


            if (dateFrom > DateTime.Now.AddMonths(12))
                return 0;


            if (dateFrom == dateTo)
                return 1;


            TimeSpan dateInterval = dateTo - dateFrom;
            int numberOfDays = dateInterval.Days;
            DateTime currentDay;
            int daysWithoutWeekend = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (dateFrom.AddDays(i));
                if (currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday)
                    continue;

                daysWithoutWeekend++;
            }

            return daysWithoutWeekend;
        }
    }
}
