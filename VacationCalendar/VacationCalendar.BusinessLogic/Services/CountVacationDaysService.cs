using DateTimeExtensions;
using DateTimeExtensions.WorkingDays;
using System.Text.RegularExpressions;
using VacationCalendar.BusinessLogic.Migrations;

namespace VacationCalendar.BusinessLogic.Services
{
    public class CountVacationDaysService : ICountVacationDaysService
    {
        /// <summary>
        /// Metoda oblicza dni wakacji, pomija soboty i niedziele
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int CountVacationDays(DateTime dateFrom, DateTime dateTo, out string message)
        {
            var culture = new WorkingDayCultureInfo("pl-PL");

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

            if (!dateFrom.IsWorkingDay(culture) || !dateTo.IsWorkingDay(culture))
            {
                message = $"Nie możesz wziąć urlopu w swięta.";
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
            int freeDays = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (dateFrom.AddDays(i));
                if (!currentDay.IsWorkingDay(culture))
                    continue;

                freeDays++;
            }

            message = $"Wystawiono wniosek. Ilość dni urlopu:";
            return freeDays;
        }

        public int CountVacationDays(DateTime dateFrom, DateTime dateTo)
        {
            var culture = new WorkingDayCultureInfo("pl-PL");

            if (dateFrom > dateTo)
                return 0;

            if (dateFrom.DayOfWeek == DayOfWeek.Saturday || dateFrom.DayOfWeek == DayOfWeek.Sunday
                || dateTo.DayOfWeek == DayOfWeek.Saturday || dateTo.DayOfWeek == DayOfWeek.Sunday)
            {
                return 0;
            }

            if (!dateFrom.IsWorkingDay(culture) || !dateTo.IsWorkingDay(culture))
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
            int freeDays = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (dateFrom.AddDays(i));
                if (!currentDay.IsWorkingDay(culture))
                    continue;

                freeDays++;
            }

            return freeDays;
        }
    }
}
