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
            
            DateTime currentDay;
            int daysWithoutWeekend = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (startDate.AddDays(i));
                if (currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday)
                    continue;

                daysWithoutWeekend++;
            }

            return daysWithoutWeekend;
        }
    }
}
