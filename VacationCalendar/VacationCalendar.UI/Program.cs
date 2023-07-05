using System;
using System.Text.RegularExpressions;
using VacationCalendar.BusinessLogic;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VacationRequest vacationRequest = new VacationRequest();
            VacationRequests vacationRequests = new VacationRequests();
            var vacationRequestsList = vacationRequests.vacationRequestsList;

            Employees employees = new Employees();
            var employeesList = employees.EmployeesList;
           
            var menu = new Menu(new string[] { "Wystaw wniosek urlopowy", "Opcja 2", "Opcja 3", "Opcja 4", "Manager", "Exit" });
            var menuPainter = new ConsoleMenuPainter(menu);

            bool esc = true;

            do
            {
                Console.WriteLine("Kalendarz urlopowy");
                Console.WriteLine("\nWybierz opcję w menu:");

                bool done = false;

                do
                {
                    // położenie menu w konsoli
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
                    var vacationDays = vacationRequest.CountVacationDays(from, to, out message);

                    Regex validateDateRegex = new Regex("^[0-9]{1,2}\\/[0-9]{1,2}\\/[0-9]{4}$");

                    var validatorFrom = validateDateRegex.IsMatch(from);
                    var validatorTo = validateDateRegex.IsMatch(to);

                    if (validatorFrom && validatorTo)
                    {
                        var employee1 = employeesList.FirstOrDefault(e => e.Id == 1);

                        VacationRequest vacation1 = new VacationRequest
                        {
                            From = DateTime.Parse(from),
                            To = DateTime.Parse(to),
                            NumberOfDays = vacationDays,
                            EmployeeId = 1
                        };

                        vacationRequestsList.Add(vacation1);

                        Console.WriteLine($"Pracownik: {employee1.FirstName} {employee1.LastName}");
                        Console.WriteLine($"Urlop od {vacation1.From.ToString("dd-MM-yy")} do {vacation1.To.ToString("dd-MM-yy")}");
                        Console.WriteLine($"{message} {vacation1.NumberOfDays}");
                    }
                    else { Console.WriteLine("Nieprawidłowy format daty"); }
                }
                if (menu.SelectedIndex == 1)
                {
                    Console.WriteLine("Wykonuje się opcja 2...");
                }
                if (menu.SelectedIndex == 2)
                {
                    Console.WriteLine("Wykonuje się opcja 3...");
                }
                if (menu.SelectedIndex == 3)
                {
                    Console.WriteLine("Wykonuje się opcja 4...");
                }
                if (menu.SelectedIndex == 4)
                {
                    Console.WriteLine("Wykonuje się opcja 5...");
                }
                if (menu.SelectedIndex == 5)
                {
                    esc = !esc;
                }

                Console.ReadKey();
                Console.Clear();

            } while (esc);
        }
    }
}