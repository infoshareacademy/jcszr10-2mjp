using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IManagerService
    {
        public Task<List<VacationRequest>> GetVacationRequests();
        public Task<VacationRequest> GetVacationRequestById(int id);
        public Task Accept(VacationRequest vacationRequest);
        public Task Reject(VacationRequest vacationRequest);
        public Task Delete(int id);
    }
}
