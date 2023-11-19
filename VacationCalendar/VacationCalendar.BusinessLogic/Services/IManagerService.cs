using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IManagerService
    {
        public Task<List<VacationRequest>> GetVacationRequests();
        public Task<VacationRequest> GetVacationRequestById(int id);
        public Task Accept(VacationRequest vacationRequest);
        public Task Reject(VacationRequest vacationRequest, string message);
        public Task Delete(int id);
        public Task<List<VacationRequest>> GetVacationRequestsByManager(Guid managerId);
        public Task<Employee> GetEmployeeByEmail(string email);
    }
}
