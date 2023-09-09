using Microsoft.EntityFrameworkCore;
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
            var employees =  _context.Employees.Include(e => e.Role).ToList();
            return employees;
        }
        public async Task<List<VacationRequest>> GetVacationRequests()
        {
            var requests = await _context.VacationRequests.Include(req => req.Employee).Include(req => req.RequestStatus).ToListAsync();
            return requests;
        }

        public async Task<List<VacationRequest>> GetVacationRequests(string email)
        {
            var requests = await _context.VacationRequests.Where(req => req.Employee.Email == email).Include(req => req.Employee).Include(req => req.RequestStatus).ToListAsync();
            return requests;
        }

        public void DeleteVacationRequest(int id)
        {
            // todo zamien na async firstordefault
            _context.VacationRequests.Remove(_context.VacationRequests.Find(id));
            _context.SaveChanges();
        }

        public void SetVacationDays(string email, int days)
        {
            var employee = _context.Employees.First(emp => emp.Email == email);
            employee.VacationDays = days;
        }
    }
}
