namespace VacationCalendar.BusinessLogic.Models
{
    public class VacationRequest
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int EmployeeId { get; set; }
        public bool isConfirmed { get; set; } = false;    
        public bool isRejected { get; set; } = false;
        public int NumberOfDays { get; set; }
    }
}
