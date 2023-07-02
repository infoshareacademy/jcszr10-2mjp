using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.UI
{
    public class Program
    {
        List<MenuOption> mainMenu = new List<MenuOption>
        {
            new MenuOption("1.Employee", () =>
            {
                new Employee(1, "Jakub", "Szot", "jakubszot17@gmail.com");

            }),
        };
        static void Main(string[] args)
        {   

        }
    }
}