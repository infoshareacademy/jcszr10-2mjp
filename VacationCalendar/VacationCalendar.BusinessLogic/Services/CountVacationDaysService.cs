using DateTimeExtensions;
using DateTimeExtensions.WorkingDays;
using NToastNotify;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class CountVacationDaysService : ICountVacationDaysService
    {
        private readonly IToastNotification _toastNotification;
        public CountVacationDaysService(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }

        public bool IsPreviusRequestContainsCurrentRequest(CreateVacationRequestDto dto, List<VacationRequest> requests)
        {
            foreach (var previousRequest in requests)
            {
                if ((dto.From <= previousRequest.To && dto.To >= previousRequest.From)
                    || (dto.From >= previousRequest.From && dto.To <= previousRequest.To))
                {
                    _toastNotification.AddWarningToastMessage("Twój nowy wniosek pokrywa się z poprzednimi.");
                    return true;
                }
            }
            return false;
        }

        public bool IsVacationDaysAfterRequest(int? vacationDays, int requestDays)
        {
            var daysAfterRequest = vacationDays - requestDays;

            if (daysAfterRequest < 0)
            {
                _toastNotification.AddWarningToastMessage("Twój wniosek przekracza ilość dni urlopu do wykorzystanaia.");
                return false;
            }
            return true;
        }

        public int CountVacationDaysAfterRequest(int? vacationDays, int requestDays)
        {
            var daysAfterRequest = vacationDays - requestDays;
            return (int)daysAfterRequest; 
        }
        /// <summary>
        /// Metoda oblicza dni wakacji, pomija soboty i niedziele
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int VacationDaysValidation(DateTime dateFrom, DateTime dateTo)
        {
            var culture = new WorkingDayCultureInfo("pl-PL");

            if (dateFrom > dateTo)
            {
                _toastNotification.AddWarningToastMessage("\"Data od\" nie może być nowsza od \"daty do\"!");
                return 0;
            }

            if (dateFrom.DayOfWeek == DayOfWeek.Saturday || dateFrom.DayOfWeek == DayOfWeek.Sunday
                || dateTo.DayOfWeek == DayOfWeek.Saturday || dateTo.DayOfWeek == DayOfWeek.Sunday)
            {
                _toastNotification.AddWarningToastMessage("Wniosek nie może zaczynać i kończyć się na sobocie lub niedzieli.");
                return 0;
            }

            if (!dateFrom.IsWorkingDay(culture) || !dateTo.IsWorkingDay(culture))
            {
                _toastNotification.AddWarningToastMessage("Nie możesz wziąć urlopu w swięta.");
                return 0;
            }

            if (dateFrom < DateTime.Now)
            {
                _toastNotification.AddWarningToastMessage("Urlop nie może być planowany wstecz ani w dniu brania urlopu.");
                return 0;
            }

            if (dateFrom > DateTime.Now.AddMonths(12))
            {
                _toastNotification.AddWarningToastMessage("Nie możesz planowac tak daleko w przyszłość.");
                return 0;
            }

            if (dateFrom == dateTo)
            {
                _toastNotification.AddSuccessToastMessage($"Wystawiono wniosek. Ilość dni urlopu: 1");
                return 1;
            }

            TimeSpan dateInterval = dateTo - dateFrom;
            int numberOfDays = dateInterval.Days;
            DateTime currentDay;
            int daysWithoutWeekend = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (dateFrom.AddDays(i));
                if (!currentDay.IsWorkingDay(culture))
                    continue;

                daysWithoutWeekend++;
            }
            _toastNotification.AddSuccessToastMessage($"Wystawiono wniosek. Ilość dni urlopu: {daysWithoutWeekend}");
            return daysWithoutWeekend;
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
            int daysWithoutWeekend = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (dateFrom.AddDays(i));
                if (!currentDay.IsWorkingDay(culture))
                    continue;

                daysWithoutWeekend++;
            }

            return daysWithoutWeekend;
        }
    }
}
