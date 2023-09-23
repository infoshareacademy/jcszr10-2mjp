using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IAdminService
    {
        public Task <List<VacationRequest>> GetVacationRequestsAsync ();
        public Task Delete(int id);
    }
}
