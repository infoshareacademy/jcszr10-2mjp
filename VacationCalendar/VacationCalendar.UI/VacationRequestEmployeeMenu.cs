using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.UI
{
    internal class VacationRequestEmployeeMenu
    {
        internal static void DisplayVacationRequestEmployeeMenu(VacationService vacationService, Employee employee)
        {
            Console.WriteLine("\nPodaj datę od kiedy? (dd/MM/rrrr)");
            string from = Console.ReadLine();
            Console.WriteLine("Podaj datę do kiedy? (dd/MM/rrrr)");
            string to = Console.ReadLine();

            string message;
            var vacationDays = vacationService.CountVacationDays(from, to, out message);

            Regex validateDateRegex = new Regex("^[0-9]{1,2}\\/[0-9]{1,2}\\/[0-9]{4}$");

            var validatorFrom = validateDateRegex.IsMatch(from);
            var validatorTo = validateDateRegex.IsMatch(to);

            if (validatorFrom && validatorTo)
            {
                VacationRequest vacation;

                if (VacationService.GetVacationRequests().Count() != 0)
                {
                    vacation = new VacationRequest
                    {
                        Id = VacationService.GetVacationRequests().Max(r => r.Id + 1),
                    };
                }
                if (VacationService.GetVacationRequests().Count() == 0)
                {
                    vacation = new VacationRequest
                    {
                        Id = 1,
                    };
                }
                vacation = new VacationRequest
                {
                    From = DateTime.Parse(from),
                    To = DateTime.Parse(to),
                    NumberOfDays = vacationDays,
                    EmployeeId = employee.Id,
                };

                if (vacationDays != 0)
                {
                    vacationService.AddVacationRequest(vacation);
                    Console.WriteLine($"Pracownik: {employee.FirstName} {employee.LastName}");
                    Console.WriteLine($"Urlop od {vacation.From.ToString("dd-MM-yy")} do {vacation.To.ToString("dd-MM-yy")}");
                    Console.WriteLine($"{message} {vacation.NumberOfDays}");
                }
                else
                {
                    Console.WriteLine($"{message}");
                }
            }
            else { Console.WriteLine("Nieprawidłowy format daty"); }
        }
    }
}
