using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IEmployeeService
    {
        // Returns List of VacationRequests
        public Task<List<VacationRequest>> GetVacationRequests();
        // Returns List of VacationRequests belonging to Employee specified in parameter
        public Task<List<VacationRequest>> GetVacationRequests(string email);
        public Task DeleteVacationRequest(int id);
        public void SetVacationDays(string email, int days);
        public Task<VacationRequest> GetVacationRequest(int id);
        public Task EditVacationRequest(EditVacationRequestDto dto);
        Task CreateVacationRequest(CreateVacationRequestDto dto);
    }
}
