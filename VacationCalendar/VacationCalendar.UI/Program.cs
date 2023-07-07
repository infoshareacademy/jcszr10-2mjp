using System;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using VacationCalendar.BusinessLogic;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VacationService vacationService = new VacationService();
            VacationRequest vacationRequest = new VacationRequest();
            VacationRequests vacationRequests = new VacationRequests();
            var vacationRequestsList = vacationRequests.vacationRequestsList;

            Employees employees = new Employees();
            var employeesList = employees.EmployeesList;

            Console.WriteLine("Zaloguj się jako:");
            Console.WriteLine("Imię:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Nazwisko:");
            string lastname = Console.ReadLine();
            Console.Clear();

            var employee = EmployeeService.LogInEmployee(firstname, lastname, employees);
            if (employee == null)
            {
                return;
            }

            var menu = new Menu(new string[] { "Wystaw wniosek urlopowy", "Manager", "Exit" });
            var menuPainter = new ConsoleMenuPainter(menu);

            bool esc = true;

            do
            {
                Console.WriteLine("Kalendarz urlopowy");
                Console.WriteLine("\nWybierz opcję w menu:");

                bool done = false;

                do
                {
                    menuPainter.Paint(2, 3);

                    var keyInfo = Console.ReadKey();

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow: menu.MoveUp(); break;
                        case ConsoleKey.DownArrow: menu.MoveDown(); break;
                        case ConsoleKey.Enter: done = true; break;
                    }
                }
                while (!done);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Wybrano: " + (menu.SelectedOption ?? "Nie wybrano opcji z menu..."));
                Console.ForegroundColor = ConsoleColor.Gray;

                if (menu.SelectedIndex == 0)
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
                        VacationRequest vacation1 = new VacationRequest
                        {
                            From = DateTime.Parse(from),
                            To = DateTime.Parse(to),
                            NumberOfDays = vacationDays,
                            EmployeeId = employee.Id
                        };

                        vacationService.AddVacationRequest(vacation1);

                        Console.WriteLine($"Pracownik: {employee.FirstName} {employee.LastName}");
                        Console.WriteLine($"Urlop od {vacation1.From.ToString("dd-MM-yy")} do {vacation1.To.ToString("dd-MM-yy")}");
                        Console.WriteLine($"{message} {vacation1.NumberOfDays}");
                    }
                    else { Console.WriteLine("Nieprawidłowy format daty"); }
                }
              
                if (menu.SelectedIndex == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Menu managera");
                    var managerMenu = new Menu(new string[] { "Wnioski", "Pracownicy", "End" });
                    var managerMenuPainter = new ConsoleMenuPainter(managerMenu);
                    bool end = false;
                    do
                    {
                        managerMenuPainter.Paint(0, 1);

                        var keyInfo2 = Console.ReadKey();

                        switch (keyInfo2.Key)
                        {
                            case ConsoleKey.UpArrow: managerMenu.MoveUp(); break;
                            case ConsoleKey.DownArrow: managerMenu.MoveDown(); break;
                            case ConsoleKey.Enter: end = true; break;
                        }
                    }
                    while (!end);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Wybrano: " + (menu.SelectedOption ?? "Nie wybrano opcji z menu..."));
                    Console.ForegroundColor = ConsoleColor.Gray;

                    if(managerMenu.SelectedIndex == 0)
                    {
                        Console.WriteLine("Wnioski:");
                        vacationService.DisplayAllVacationRequests();
                    }
                    if(managerMenu.SelectedIndex == 1)
                    {
                        Console.WriteLine("Pracownicy");
                    }
                }
                if (menu.SelectedIndex == 2)
                {
                    esc = !esc;
                }

                Console.ReadKey();
                Console.Clear();

            } while (esc);
        }
    }
}