using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
        private readonly VacationCalendarDbContext _dbContext;

        public AdminService(VacationCalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(int id)
        {
            var request = await _dbContext.VacationRequests.FindAsync(id);
            _dbContext.VacationRequests.Remove(request);
            _dbContext.SaveChanges();
        }

        public async Task<List<VacationRequest>> GetVacationRequestsAsync()
        {
            var requests = await _dbContext.VacationRequests.Include(req => req.Employee).Include(req => req.RequestStatus).ToListAsync();
            return requests;
        }
    }
}
