using Newtonsoft.Json;
using System.Diagnostics;
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

        public List<string> GetAllVacationRequestsToString(int id)
        {
            var vacationRequestList= GetVacationRequests();
     
            List<Employee> employees = EmployeeService.GetEmployees().ToList();
            var employeesByManager = employees.Where(e => e.ManagerId == id).ToList();
            var listOfEmployeesIds = employeesByManager.Select(e => e.Id).ToList();

            List<VacationRequest> vacationRequestsByManager = new List<VacationRequest>();

            foreach (var v in vacationRequestList)
            {
                if (listOfEmployeesIds.Contains(v.EmployeeId))
                {
                    vacationRequestsByManager.Add(v);
                }
            }

            List<string> vacRequests = new List<string>();

            foreach (var request in vacationRequestsByManager)
            {
                var employeeLastName = EmployeeService.GetEmployees().FirstOrDefault(e => e.Id == request.EmployeeId).LastName;
                   vacRequests.Add(
                        $" Id wniosku: {request.Id};" +
                        $" Pracownik: id {request.EmployeeId} {employeeLastName};" +
                        $" Wniosek od: {request.From.ToString("dd-MM-yy")};" +
                        $" do: {request.To.ToString("dd-MM-yy")};" +
                        $" Dni: {request.NumberOfDays};" +
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
            DateTime dateFrom;
            bool isDateFromGoodFormat = DateTime.TryParse(from, out dateFrom);
            vacationRequest.From = dateFrom;

            DateTime dateTo;
            bool isDateToGoodFormat = DateTime.TryParse(to, out dateTo);
            vacationRequest.To = dateTo;

            if (!isDateToGoodFormat || !isDateFromGoodFormat)
            {
                message = "Nieprawidłowy format daty!";
                return 0;
            }

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

            if (dateFrom.Day < DateTime.Now.Day)
            {
                message = "Urlop nie może być planowany wstecz.";
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
    }
}
