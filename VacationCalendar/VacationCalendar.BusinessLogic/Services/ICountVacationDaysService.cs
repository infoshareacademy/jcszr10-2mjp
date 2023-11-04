using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface ICountVacationDaysService
    {
        public int VacationDaysValidation(DateTime dateFrom, DateTime dateTo);
        public int CountVacationDays(DateTime dateFrom, DateTime dateTo);
        public bool IsPreviusRequestContainsCurrentRequest(CreateVacationRequestDto dto, List<VacationRequest> requests);
        public bool IsVacationDaysAfterRequest(int? vacationDays, int requestDays);
    }
}
