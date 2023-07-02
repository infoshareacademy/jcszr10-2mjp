using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.UI
{
    public class Program
    {
        static Menu menu = new(); 
        static VacationRequest vacRequest = new();
        static Employee employee = new();
        static Supervisor supervisor = new();

        static List<MenuOption> menuOptions = new()
        {
            new MenuOption("1.Employee", () =>
            {
                Console.Clear();
                menu.Options = employeeOptions;
                employee.ID = 1;
                employee.FirstName = "Jakub";
                employee.LastName = "Szot";
                employee.Email = "jakubszot17@gmail.com";
            }),
            new MenuOption("2.Supervisor", () =>
            {
                Console.Clear();
                menu.Options = supervisorOptions;
                supervisor.ID = 2;
                supervisor.FirstName = "Piotr";
                supervisor.LastName = "Tryfon";
                supervisor.Email = "p.tryfon@gmail.com";
            })
        };

        static List<MenuOption> employeeOptions = new()
        {
            new MenuOption("1.Info", () =>
            {
                Console.Clear();
                Console.WriteLine(employee.ToString());
                Console.ReadKey();
                Console.Clear();
            }),

            new MenuOption("2.Wniosek", () =>
            {
                Console.Clear();
                vacRequest.Emp = employee;
                vacRequest.From = new DateTime(2023, 07, 02);
                vacRequest.To = new DateTime(2023, 07, 12);
                vacRequest.Status = true;
                Console.ReadKey();
                Console.Clear();
            }),

            new MenuOption("3.Powrot", () =>
            {
                Console.Clear();
                menu.Options = menuOptions;
            })
        };

        static List<MenuOption> supervisorOptions = new()
        {
            new MenuOption("1.Info", () =>
            {
                Console.Clear();
                Console.WriteLine(supervisor.ToString());
                Console.ReadKey();
                Console.Clear();
            }),

            new MenuOption("2.Wnioski", () =>
            {
                Console.Clear();
                Console.WriteLine(vacRequest.ToString());
                Console.ReadKey();
                Console.Clear();
            }),

            new MenuOption("3.Wniosek zakoncz", () =>
            {
                Console.Clear();
                supervisor.Change(vacRequest);
                Console.ReadKey();
                Console.Clear();
            }),

            new MenuOption("3.Powrot", () =>
            {
                Console.Clear();
                menu.Options = menuOptions;
            })
        };
        static void Main(string[] args)
        {
            menu.Options = menuOptions;
            menu.Run();
        }
    }
}