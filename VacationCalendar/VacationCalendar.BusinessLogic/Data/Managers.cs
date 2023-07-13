using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Data
{
    public class Managers
    {
        public List<Manager> ManagersList { get; set; } = new List<Manager>
        {
            new Manager()
            {
                Id = 1,
                FirstName = "Olaf",
                LastName = "Kowal",
            },
             new Manager()
            {
                Id = 2,
                FirstName = "Anna",
                LastName = "Nowak",
            }
        };
    }
}
