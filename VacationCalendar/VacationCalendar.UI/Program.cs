using System.Text.RegularExpressions;
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

            Employees employees = new Employees();
            Managers managers = new Managers();

            var menu = new Menu(new string[] { "Nowy wniosek pracownika", "Manager", "Exit" });
            var menuPainter = new ConsoleMenuPainter(menu);

            bool esc = true;

            while(esc) 
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
                    Console.WriteLine("Zaloguj się jako:");
                    Console.WriteLine("Imię:");
                    string firstname = Console.ReadLine();
                    Console.WriteLine("Nazwisko:");
                    string lastname = Console.ReadLine();
                    Console.Clear();

                    var employee = EmployeeService.LogInEmployee(firstname, lastname, employees);

                    if (employee == null)
                    {
                        Console.WriteLine("Nie ma takiego pracownika.");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }

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
                        VacationRequest vacation = new VacationRequest
                        {
                            Id = vacationService.GetNumberOfVacationRequests() + 1,
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

                if (menu.SelectedIndex == 1)
                {
                    Console.WriteLine("Zaloguj się jako:");
                    Console.WriteLine("Imię:");
                    string firstname = Console.ReadLine();
                    Console.WriteLine("Nazwisko:");
                    string lastname = Console.ReadLine();
                    Console.Clear();

                    var manager = ManagerService.LogInManager(firstname, lastname, managers);

                    if (manager == null)
                    {
                        Console.WriteLine("Nie ma takiego managera.");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }

                    bool managerExit = false;
                    while (!managerExit)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Menu managera");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        var managerMenu = new Menu(new string[] { "Wnioski", "Pracownicy", "Exit" });
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

                        if (managerMenu.SelectedIndex == 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("WNIOSKI:");
                            Console.ForegroundColor = ConsoleColor.Gray;

                            var vacReqListToStr = vacationService.GetAllVacationRequestsToString()
                                .Where(r=>!r.Contains("Exit")).ToList();                       

                            var numberOfElementsInList = vacReqListToStr.Count;
                            if (numberOfElementsInList == 0)
                            {
                                vacReqListToStr = new List<string> { "Brak wniosków." };
                            }

                            var requestMenu = new Menu(vacReqListToStr);
                            var requestMenuPainter = new ConsoleMenuPainter(requestMenu);                    
                            
                            bool isDone = false;
                            do
                            {
                                requestMenuPainter.Paint(0, 1);

                                var keyInfo3 = Console.ReadKey();

                                switch (keyInfo3.Key)
                                {
                                    case ConsoleKey.UpArrow: requestMenu.MoveUp(); break;
                                    case ConsoleKey.DownArrow: requestMenu.MoveDown(); break;
                                    case ConsoleKey.Enter: isDone = true; break;                             
                                }
                            }
                            while (!isDone);

                            if (requestMenu.SelectedOption == null)
                            {
                                Console.WriteLine("Exit");
                                isDone = !isDone;
                            }
                            if (requestMenu.SelectedOption == "Exit")
                            {                            
                                isDone = !isDone;
                            }

                            for (var i = 0; i < numberOfElementsInList; i++)
                            {
                                if (requestMenu.SelectedIndex == i && requestMenu.SelectedIndex < requestMenu.Items.Count()-1)
                                {
                                    bool isRequestStatusContinue = true;
                                    while (isRequestStatusContinue)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine($"Wniosek id {i + 1}:");
                                        Console.ForegroundColor = ConsoleColor.Gray;

                                        var changeRequestMenu = new Menu(new[] { "Zatwierdź", "Odrzuć", "Exit" });
                                        var changeRequestMenuPainter = new ConsoleMenuPainter(changeRequestMenu);

                                        bool isChangeRequestMenuDone = false;
                                        do
                                        {
                                            changeRequestMenuPainter.Paint(0, 1);

                                            var keyInfo4 = Console.ReadKey();

                                            switch (keyInfo4.Key)
                                            {
                                                case ConsoleKey.UpArrow: changeRequestMenu.MoveUp(); break;
                                                case ConsoleKey.DownArrow: changeRequestMenu.MoveDown(); break;
                                                case ConsoleKey.Enter: isChangeRequestMenuDone = true; break;
                                            }
                                        }
                                        while (!isChangeRequestMenuDone);

                                        if (changeRequestMenu.SelectedIndex == 0)
                                        {
                                            vacationService.ConfirmRequest(i);
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Console.WriteLine("Wniosek został zatwierdzony.");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        if (changeRequestMenu.SelectedIndex == 1)
                                        {
                                            vacationService.RejectRequest(i);
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Console.WriteLine("Wniosek został odrzucony.");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        if (changeRequestMenu.SelectedIndex == 2)
                                        {
                                            isRequestStatusContinue = false;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Clear();
                                }
                            }
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (managerMenu.SelectedIndex == 1)
                        {
                            Console.WriteLine("Pracownicy:");
                            EmployeeService.GetEmployees(employees);
                            Console.WriteLine("\nMenadżerowie:");
                            ManagerService.GetManagers(managers);
                            Console.ReadKey();
                        }
                        if (managerMenu.SelectedIndex == 2)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Gray;
                            managerExit = true;
                        }
                    }
                }

                if (menu.SelectedIndex == 2)
                {
                    esc = !esc;
                }

                Console.ReadKey();
                Console.Clear();

            };
        }
    }
}