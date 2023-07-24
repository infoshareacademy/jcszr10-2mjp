using Newtonsoft.Json;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class VacationService
    {
        VacationRequest vacationRequest = new VacationRequest();

        static string path = $@"{DirectoryPathProvider.GetSolutionDirectoryInfo().FullName}\VacationCalendar.BusinessLogic\Data\vacationRequests.json";
        public static List<VacationRequest> GetVacationRequests()
        {
            var vacationRequestSerialized = File.ReadAllText(path);
            List<VacationRequest> requests = JsonConvert.DeserializeObject<List<VacationRequest>>(vacationRequestSerialized);
            return requests;
        }
        public void ConfirmRequest(int id)
        {
            RequestStatus confirmed = RequestStatus.Confirmed;
            try
            {
                var requests = GetVacationRequests();
                var request = requests.FirstOrDefault(r => r.Id == id);
                if(request != null)
                {
                    request.requestStatus = confirmed;
                    var json = JsonConvert.SerializeObject(requests);
                    File.WriteAllText(path, json);
                }
                else
                {
                    Console.WriteLine("Nie znaleziono wniosku.");
                }           
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            } 
        }
        public void RejectRequest(int id)
        {
            RequestStatus rejected = RequestStatus.Rejected;
            try
            {
                var requests = GetVacationRequests();
                var request = requests.FirstOrDefault(r => r.Id == id);
                if (request != null)
                {
                    request.requestStatus = rejected;
                    var json = JsonConvert.SerializeObject(requests);
                    File.WriteAllText(path, json);
                }
                else
                {
                    Console.WriteLine("Nie znaleziono wniosku.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }
        public void AddVacationRequest(VacationRequest vacationRequest)
        {
            var requests = GetVacationRequests();
            
            vacationRequest.Id = requests.Count() + 1;
            requests.Add(vacationRequest);

            var json = JsonConvert.SerializeObject(requests);
            File.WriteAllText(path, json);
        }
        public int GetNumberOfVacationRequests()
        {
            return GetVacationRequests().Count();
        }

        public List<string> GetAllVacationRequestsToString()
        {
            var vacationRequestList = GetVacationRequests();

            List<string> vacRequests = new List<string>();

            foreach (var request in vacationRequestList)
            { 
               vacRequests.Add(
                    $" Id wniosku: {request.Id}" +
                    $" Id pracownika: {request.EmployeeId}" +
                    $" Wniosek od: {request.From.ToString("dd-MM-yy")}" +
                    $" do: {request.To.ToString("dd-MM-yy")} " +
                    $" Dni: {request.NumberOfDays}" +
                    $" Status wniosku: {request.requestStatus}");            
            }
            vacRequests.Add("\nExit");
            return vacRequests;
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
                message = "Nieprawidłowy format daty!";
                return 0;
            }

            if (dateFromValue > dateToValue)
            {
                message = "\"Data od\" nie może być nowsza od \"daty do\"!";
                return 0;
            }      

            if (dateFromValue.DayOfWeek == DayOfWeek.Saturday || dateFromValue.DayOfWeek == DayOfWeek.Sunday
                || dateToValue.DayOfWeek == DayOfWeek.Saturday || dateToValue.DayOfWeek == DayOfWeek.Sunday)
            {
                message = $"Wniosek nie może zaczynać i kończyć się na sobocie lub niedzieli.";
                return 0;
            }

            if (dateFromValue.Day < DateTime.Now.Day)
            {
                message = "Urlop nie może być planowany wstecz.";
                return 0;
            }

            if (dateFromValue > DateTime.Now.AddMonths(12))
            {
                message = "Nie możesz planowac tak daleko w przyszłość.";
                return 0;
            }

            if (dateFromValue == dateToValue)
            {
                message = $"Wystawiono wniosek. Ilość dni urlopu:";
                return 1;
            }

            TimeSpan dateInterval = dateToValue - dateFromValue;
            int numberOfDays = dateInterval.Days;
            DateTime currentDay;
            int daysWithoutWeekend = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (dateFromValue.AddDays(i));
                if (currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday)
                    continue;

                daysWithoutWeekend++;
            }
            message = $"Wystawiono wniosek. Ilość dni urlopu:";
            return daysWithoutWeekend;
        }
    }
}
