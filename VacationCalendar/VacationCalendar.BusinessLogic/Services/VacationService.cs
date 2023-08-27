using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class VacationService
    {
        
        public void ConfirmRequest(int id)
        {
           
        }
        public void RejectRequest(int id)
        {
           
        }
        public void AddVacationRequest(VacationRequest vacationRequest)
        {
            
        }
     

        

        
        /// <summary>
        /// Metoda oblicza dni wakacji, pomija soboty i niedziele
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        //public int CountVacationDays(string from, string to, out string message)
        //{
        //    DateTime dateFrom;
        //    bool isDateFromGoodFormat = DateTime.TryParse(from, out dateFrom);
        //    vacationRequest.From = dateFrom;

        //    DateTime dateTo;
        //    bool isDateToGoodFormat = DateTime.TryParse(to, out dateTo);
        //    vacationRequest.To = dateTo;

        //    if (!isDateToGoodFormat || !isDateFromGoodFormat)
        //    {
        //        message = "Nieprawidłowy format daty!";
        //        return 0;
        //    }

        //    if (dateFrom > dateTo)
        //    {
        //        message = "\"Data od\" nie może być nowsza od \"daty do\"!";
        //        return 0;
        //    }      

        //    if (dateFrom.DayOfWeek == DayOfWeek.Saturday || dateFrom.DayOfWeek == DayOfWeek.Sunday
        //        || dateTo.DayOfWeek == DayOfWeek.Saturday || dateTo.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        message = $"Wniosek nie może zaczynać i kończyć się na sobocie lub niedzieli.";
        //        return 0;
        //    }

        //    if (dateFrom < DateTime.Now)
        //    {
        //        message = "Urlop nie może być planowany wstecz ani w dniu brania urlopu.";
        //        return 0;
        //    }

        //    if (dateFrom > DateTime.Now.AddMonths(12))
        //    {
        //        message = "Nie możesz planowac tak daleko w przyszłość.";
        //        return 0;
        //    }

        //    if (dateFrom == dateTo)
        //    {
        //        message = $"Wystawiono wniosek. Ilość dni urlopu:";
        //        return 1;
        //    }

        //    TimeSpan dateInterval = dateTo - dateFrom;
        //    int numberOfDays = dateInterval.Days;
        //    DateTime currentDay;
        //    int daysWithoutWeekend = 0;
        //    for (int i = 0; i <= numberOfDays; i++)
        //    {
        //        currentDay = (dateFrom.AddDays(i));
        //        if (currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday)
        //            continue;

        //        daysWithoutWeekend++;
        //    }
        //    message = $"Wystawiono wniosek. Ilość dni urlopu:";
        //    return daysWithoutWeekend;
        //}
    }
}
