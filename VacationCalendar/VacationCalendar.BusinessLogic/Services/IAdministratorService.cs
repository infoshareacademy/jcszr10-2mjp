using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IAdministratorService
    {
        public bool LogIn(string username, string password);
    }
}
