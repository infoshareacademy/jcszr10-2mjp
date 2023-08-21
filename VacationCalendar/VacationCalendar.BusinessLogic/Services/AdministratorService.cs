using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Data;

namespace VacationCalendar.BusinessLogic.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly VacationCalendarDbContext _context;

        public AdministratorService(VacationCalendarDbContext context)
        {
            _context = context;
        }

        public bool LogIn(string login, string password)
        {
            var adminLogin = _context.Administrators.FirstOrDefault(a => a.Login == login && a.Password == password);
            if (adminLogin != null)
            {
                return true;
            }
            return false;
        }

        
    }
}
