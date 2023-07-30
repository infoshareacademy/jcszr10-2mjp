using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VacationService vacationService = new VacationService();

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
                    string firstname = Console.ReadLine().Trim();
                    Console.WriteLine("Nazwisko:");
                    string lastname = Console.ReadLine().Trim();
                    Console.Clear();

                    var employee = EmployeeService.LogInEmployee(firstname, lastname);

                    if (employee == null)
                    {
                        Console.WriteLine("Nie ma takiego pracownika.");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }

                    EmployeeMenu.DisplayEmployeeMenu(vacationService, employee);
                }

                if (menu.SelectedIndex == 1)
                {
                    Console.WriteLine("Zaloguj się jako:");
                    Console.WriteLine("Imię:");
                    string firstname = Console.ReadLine().Trim();
                    Console.WriteLine("Nazwisko:");
                    string lastname = Console.ReadLine().Trim();
                    Console.Clear();

                    var manager = ManagerService.LogInManager(firstname, lastname);

                    if (manager == null)
                    {
                        Console.WriteLine("Nie ma takiego managera.");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }
                    var managerId = manager.Id;
                    bool managerExit = false;
                    while (!managerExit)
                        managerExit = ManagerMenu.DisplayManagerMenu(vacationService, managerExit, managerId);
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