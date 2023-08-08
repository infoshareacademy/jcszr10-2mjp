namespace VacationCalendar.BusinessLogic.Models
{
    public class VacationRequest
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public RequestStatus RequestStatus { get; set; } = RequestStatus.InProgress;
        public int NumberOfDays { get; set; }
    }
}
