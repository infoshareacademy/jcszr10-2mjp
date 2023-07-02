using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.UI
{
    public class Program
    {
        public static Employee emp = new Employee(1, "Jakub", "Szot", "xdd");
        public static List<MenuOption> mainOptions = new List<MenuOption>
        {
            new MenuOption("1.Employee", (vr) =>
            {
                new Menu(empOptions).Run();
            }),
            new MenuOption("2.Supervisor", (vr) =>
            {
                new Supervisor(1, "Piotr", "Tryfon", "p.tryfon@gmail.com");

            }),
        };

        public static List<MenuOption> empOptions= new List<MenuOption>
        {
            new MenuOption("1.Wniosek", (vr) =>
            {
                emp.Request(new DateTime(2023, 07, 02), new DateTime(2023, 07, 09));
            }),
            new MenuOption("2.Info", (vr) =>
            {
                new Supervisor(1, "Piotr", "Tryfon", "p.tryfon@gmail.com");

            }),

            new MenuOption("3.Powrot", (vr) =>
            {
                new Menu(mainOptions).Run();
            }),
        };

        
        static void Main(string[] args)
        {
            Menu mainMenu = new Menu(mainOptions);
            mainMenu.Run();
        }
    }
}