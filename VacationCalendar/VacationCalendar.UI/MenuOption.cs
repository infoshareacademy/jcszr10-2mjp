﻿using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.UI
{
    public class MenuOption
    {
        public string Name { get; set; }
        public Action Action { get; set; }
        public MenuOption(string name, Action action)
        {
            Name = name;
            Action = action;
        }
    }
}
