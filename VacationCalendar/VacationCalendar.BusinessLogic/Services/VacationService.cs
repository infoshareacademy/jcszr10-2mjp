using System;
using System.Security.Cryptography.X509Certificates;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class VacationService
    {
        VacationRequest vacationRequest = new VacationRequest();
        VacationRequests vacationRequests = new VacationRequests();
        public void AddVacationRequest(VacationRequest vacationRequest)
        {
            vacationRequests.vacationRequestsList.Add(vacationRequest);
        }

        public int GetNumberOfVacationRequests()
        {
            return vacationRequests.vacationRequestsList.Count();
        }

        public List<string> GetAllVacationRequestsToString()
        {
            var vacationRequestList = vacationRequests.vacationRequestsList;

            List<string> vacRequests = new List<string>();

            foreach (var request in vacationRequestList)
            { 
               vacRequests.Add(
                    $" Id pracownika: {request.EmployeeId}" +
                    $" Id wniosku: {request.Id}" +
                    $" Wniosek od: {request.From.ToString("dd-MM-yy")}" +
                    $" do: {request.To.ToString("dd-MM-yy")} " +
                    $" Dni: {request.NumberOfDays}" +
                    $" Czy potwierdzony: {request.isConfirmed}");            
            }

            return vacRequests;
        }

        public List<VacationRequest> GetVacationRequests()
        {
            return vacationRequests.vacationRequestsList;
        }
        
        /// <summary>
        /// Metoda oblicza dni wakacji, pomija soboty i niedziele
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int CountVacationDays(string from, string to, out string message)
        {
            DateTime dateFromValue;
            string dateStringFrom = from;
            bool isDateFromGoodFormat = DateTime.TryParse(dateStringFrom, out dateFromValue);
            vacationRequest.From = dateFromValue;

            DateTime dateToValue;
            string dateStringTo = to;
            bool isDateToGoodFormat = DateTime.TryParse(dateStringTo, out dateToValue);
            vacationRequest.To = dateToValue;

            if (!isDateToGoodFormat || !isDateFromGoodFormat)
            {
                message = "Nieprawidłowy format daty! Dni urlopu:";
                return 0;
            }

            if (dateFromValue >= dateToValue)
            {
                message = "\"Data od\" nie może być nowsza od \"daty do\"! Dni urlopu:";
                return 0;
            }

            if (dateFromValue < DateTime.Now)
            {
                message = "Urlop nie może być planowany wstecz. Dni urlopu:";
                return 0;
            }

            if (dateFromValue > DateTime.Now.AddMonths(12))
            {
                message = "Nie możesz planowac tak daleko w przyszłość. Dni urlopu:";
                return 0;
            }

            var numberOfDays = vacationRequest.NumberOfDaysSpan.Days;

            List<DateTime> allDays = new List<DateTime>
            {
                dateFromValue
            };

            for (int i = 1; i <= numberOfDays; i++)
            {
                DateTime tmpDate;
                tmpDate = dateFromValue.AddDays(i);
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

            if (daysWithoutWeekend.Count > 30)
            {
                message = "Trochę za dużo tego urlopu... Dni urlopu:";
                return 0;
            }

            message = $"Wystawiono wniosek. Ilość dni urlopu:";

            return daysWithoutWeekend.Count;
        }
    }
}
