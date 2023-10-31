using VacationCalendar.BusinessLogic.Data;

namespace VacationCalendar.BusinessLogic.Services
{
    public class VacationService : IVacationService
    {
        private readonly VacationCalendarDbContext _dbContext;

        public VacationService(VacationCalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
