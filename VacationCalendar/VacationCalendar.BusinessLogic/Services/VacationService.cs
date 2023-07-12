using Newtonsoft.Json;
using System.Net.WebSockets;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Providers;

namespace VacationCalendar.BusinessLogic.Services
{
    public class VacationService
    {
        VacationRequest vacationRequest = new VacationRequest();

        static string path = $@"{DirectoryPathProvider.GetSolutionDirectoryInfo().FullName}\VacationCalendar.BusinessLogic\Data\vacationRequests.json";
        private static List<VacationRequest> DeserializeVacationRequests()
        {
            var vacationRequestSerialized = File.ReadAllText(path);
            List<VacationRequest> requests = JsonConvert.DeserializeObject<List<VacationRequest>>(vacationRequestSerialized);
            return requests;
        }
        public void ChangeRequestStatus(int id)
        {
            try
            {
                var requests = DeserializeVacationRequests();
                var request = requests.FirstOrDefault(r => r.Id == id + 1);
                request.isConfirmed = !request.isConfirmed;
                var json = JsonConvert.SerializeObject(requests);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            } 
        }
        public void AddVacationRequest(VacationRequest vacationRequest)
        {
            var requests = DeserializeVacationRequests();
            if(requests == null )
            {
                var text = JsonConvert.SerializeObject(vacationRequest);
                File.WriteAllText(path, text);
            }
            else
            {
                int lastId = requests.Last().Id;
                vacationRequest.Id = lastId + 1;
                requests.Add(vacationRequest);

                var json = JsonConvert.SerializeObject(requests);
                File.WriteAllText(path, json);
            }  
        }

        public int GetNumberOfVacationRequests()
        {
            return DeserializeVacationRequests().Count();
        }

        public List<string> GetAllVacationRequestsToString()
        {
            var vacationRequestList = DeserializeVacationRequests();

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
            return DeserializeVacationRequests();
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

            if (dateFromValue > dateToValue)
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

            var numberOfDays = vacationRequest.To.Subtract(vacationRequest.From).Days;

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
