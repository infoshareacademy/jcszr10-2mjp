using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly VacationCalendarDbContext _context;
        public EmployeeService(VacationCalendarDbContext context)
        {
                _context = context;
        }
        public List<Employee> GetAll()
        {
            var employees =  _context.Employees.Include(e=>e.Manager).ToList();
            return employees;
        }
    }
}
