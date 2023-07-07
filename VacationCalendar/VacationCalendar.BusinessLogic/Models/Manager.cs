
namespace VacationCalendar.BusinessLogic.Models
{
    internal class Manager : Employee
    {
        public void ConformVacation(VacationRequest vacationRequest)
        {
            vacationRequest.isConfirmed = true;
        }
    }
}
