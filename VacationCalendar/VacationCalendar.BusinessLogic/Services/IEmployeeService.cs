using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IEmployeeService
    {
        public List<Employee> GetAll();

        // Returns List of VacationRequests
        public Task<List<VacationRequest>> GetVacationRequests();
        // Returns List of VacationRequests belonging to Employee specified in parameter
        public Task<List<VacationRequest>> GetVacationRequests(string email);

        public void DeleteVacationRequest(int id);

        public void SetVacationDays(string email, int days);

        Task<VacationRequest> GetVacationRequest(int id);

    }
}
