using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.UI
{
    public class Menu
    {
        public List<MenuOption> Options { get; set; }
        public int SelectedIndex { get; set; }
        public Menu(List<MenuOption> options) 
        { 
            Options = options;
            SelectedIndex = 0;
        }
        public void Run()
        {
            bool exit = false;
            do
            {
                Display();
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        MoveDown();
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Options[SelectedIndex].Action(new VacationRequest(new Employee(2, "qwq", "s", "d"), new DateTime(2023, 11, 02), new DateTime(2023, 12, 09)));
                        break;
                }
            } while (!exit);

        }
        public void Display()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                Console.SetCursorPosition(0, i);
                var color = SelectedIndex == i ? ConsoleColor.Green : ConsoleColor.Gray;
                Console.ForegroundColor = color;
                Console.WriteLine(Options[i].Name);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        public void MoveUp() => SelectedIndex = Math.Max(SelectedIndex - 1, 0);
        public void MoveDown() => SelectedIndex = Math.Min(SelectedIndex + 1, Options.Count - 1);
    }
}
