using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class ManagerService : IManagerService
    {
        private readonly VacationCalendarDbContext _context;
        public ManagerService(VacationCalendarDbContext context)
        {
            _context = context;
        }
        public async Task<List<VacationRequest>> GetVacationRequests()
        {
            var requests = await _context.VacationRequests.Include(req => req.Employee).Include(req => req.RequestStatus).ToListAsync();
            return requests;
        }
        public async Task<VacationRequest> GetVacationRequestById(int id)
        {
            var request = await _context.VacationRequests.FirstOrDefaultAsync(req => req.Id == id);
            return request;
        }
        public async Task Accept(VacationRequest vacationRequest)
        {
            vacationRequest.RequestStatusId = 2;
            await _context.SaveChangesAsync();
        }

        public async Task Reject(VacationRequest vacationRequest)
        {
            vacationRequest.RequestStatusId = 3;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var request = await _context.VacationRequests.FindAsync(id);
            _context.VacationRequests.Remove(request);
            _context.SaveChanges();
        }
    }
}
