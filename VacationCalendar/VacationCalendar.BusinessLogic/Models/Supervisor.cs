namespace VacationCalendar.BusinessLogic.Models
{
    public class Supervisor : Employee
    {
        public Supervisor (int id, string firstName, string lastName, string email) : base(id, firstName, lastName, email){ }
        public void Change (VacationRequest request)
        {
            request.Status = Status(false);
        }
        private bool Status (bool Decision)
        {
            return Decision;
        }
    }
}
