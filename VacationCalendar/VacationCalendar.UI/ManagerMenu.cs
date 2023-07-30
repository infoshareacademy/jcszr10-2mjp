using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.UI
{
    public class ManagerMenu
    {
        internal static bool DisplayManagerMenu(VacationService vacationService, bool managerExit, int managerId)
        {
            {
                var manager = ManagerService.GetManagers().Where(m => m.Id == managerId).FirstOrDefault();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Menu managera: {manager.FirstName} {manager.LastName}");
                Console.ForegroundColor = ConsoleColor.Gray;
                var managerMenu = new Menu(new string[] { "Wnioski", "Pracownicy", "Exit" });
                var managerMenuPainter = new ConsoleMenuPainter(managerMenu);
                bool end = false;
                do
                {
                    managerMenuPainter.Paint(0, 1);

                    var keyInfo = Console.ReadKey();

                    switch (keyInfo.Key)
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

                    var vacReqListToStr = vacationService.GetAllVacationRequestsToString(managerId);

                    var vacReqListToStrWithoutExit = vacationService.GetAllVacationRequestsToString(managerId)
                        .Where(r => !r.Contains("Exit")).ToList();

                    if (vacReqListToStrWithoutExit.Count == 0)
                    {
                        vacReqListToStr = new List<string> { "Brak wniosków." };
                    }

                    var requestMenu = new Menu(vacReqListToStr);
                    var requestMenuPainter = new ConsoleMenuPainter(requestMenu);

                    bool isDone = false;
                    do
                    {
                        requestMenuPainter.Paint(0, 1);

                        var keyInfo = Console.ReadKey();

                        switch (keyInfo.Key)
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

                    for (var i = 0; i < vacReqListToStr.Count; i++)
                    {
                        if (requestMenu.SelectedIndex == i && requestMenu.SelectedIndex < requestMenu.Items.Count() - 1)
                        {
                            bool isRequestStatusContinue = true;
                            while (isRequestStatusContinue)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine($"Wniosek {i + 1}:");
                                Console.ForegroundColor = ConsoleColor.Gray;

                                var changeRequestMenu = new Menu(new[] { "Zatwierdź", "Odrzuć", "Exit" });
                                var changeRequestMenuPainter = new ConsoleMenuPainter(changeRequestMenu);

                                bool isChangeRequestMenuDone = false;
                                do
                                {
                                    changeRequestMenuPainter.Paint(0, 1);

                                    var keyInfo = Console.ReadKey();

                                    switch (keyInfo.Key)
                                    {
                                        case ConsoleKey.UpArrow: changeRequestMenu.MoveUp(); break;
                                        case ConsoleKey.DownArrow: changeRequestMenu.MoveDown(); break;
                                        case ConsoleKey.Enter: isChangeRequestMenuDone = true; break;
                                    }
                                }
                                while (!isChangeRequestMenuDone);

                                if (changeRequestMenu.SelectedIndex == 0)
                                {
                                    vacationService.ConfirmRequest(i + 1);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine("Wniosek został zatwierdzony.");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                if (changeRequestMenu.SelectedIndex == 1)
                                {
                                    vacationService.RejectRequest(i + 1);
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
                    EmployeeService.GetEmployeesToString();
                    Console.WriteLine("\nMenadżerowie:");
                    ManagerService.GetManagersToString();
                    Console.ReadKey();
                }
                if (managerMenu.SelectedIndex == 2)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    managerExit = true;
                }
            }

            return managerExit;
        }
    }
}
