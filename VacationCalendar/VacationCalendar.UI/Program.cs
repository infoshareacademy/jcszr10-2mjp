using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kalendarz urlopowy");

            var menu = new Menu(new string[] { "Wystaw wniosek urlopowy", "Opcja_2", "Opcja_3", "Opcja_4", "Opcja_5", "Opcja_6" });
            var menuPainter = new ConsoleMenuPainter(menu);

            bool done = false;

            do
            {
                // położenie menu
                menuPainter.Paint(1, 3);

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

                PrepareVacationRequest(from, to) ;
            }
            if (menu.SelectedIndex == 1)
            {
                Console.WriteLine("Opcja 2");
            }
        }

        private static void PrepareVacationRequest(string from, string to)
        {
            VacationRequest vacationRequest = new VacationRequest();

            DateTime dateValueFrom;
            string dateStringFrom = from;
            DateTime.TryParse(dateStringFrom, out dateValueFrom);
            vacationRequest.From = dateValueFrom;

            DateTime dateValueTo;
            string dateStringTo = to;
            DateTime.TryParse(dateStringTo, out dateValueTo);
            vacationRequest.To = dateValueTo;

            var days = vacationRequest.NumberOfDays.Days;
            Console.WriteLine($"Dni urlopu: {days}");
        }
    }
}