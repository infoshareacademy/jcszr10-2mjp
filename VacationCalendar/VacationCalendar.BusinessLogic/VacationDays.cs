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

            if (!DateTime.TryParse(from, out DateTime startDate) || !DateTime.TryParse(to, out DateTime endDate))
            {
                // Wyrzuca wyjątek jeżeli nie uda się sparsować Stringa z Datą na typ DateTime
                throw new ArgumentException("Nieprawiłowy format daty!");
            }

            if (endDate < startDate)
            {
                throw new ArgumentException("Data koncowa musi byc późniejsza niż data startowa!");
            }

            VacationRequest vacationRequest = new VacationRequest(startDate, endDate);

            TimeSpan dateInterval = vacationRequest.To - vacationRequest.From;
            int numberOfDays = dateInterval.Days;

            List<DateTime> daysWithoutWeekend = new List<DateTime>
            {
                startDate
            };

            DateTime nextDay;
            for (int i = 1; i <= numberOfDays; i++)
            {
                nextDay = (daysWithoutWeekend[0].AddDays(i));
                if (nextDay.DayOfWeek == DayOfWeek.Sunday || nextDay.DayOfWeek == DayOfWeek.Saturday)
                    continue;

                daysWithoutWeekend.Add(nextDay);
            }

            return daysWithoutWeekend.Count;
        }
    }
}
