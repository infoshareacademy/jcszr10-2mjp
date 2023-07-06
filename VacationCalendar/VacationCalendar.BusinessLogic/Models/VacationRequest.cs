namespace VacationCalendar.BusinessLogic.Models
{
    public class VacationRequest
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public bool Confirmed { get; set; } = false;

        public TimeSpan NumberOfDaysSpan 
        {
            get 
            {  
                return To.Subtract(From); 
            }
            set
            {
                NumberOfDaysSpan = value;
            }
        }
        public int NumberOfDays { get; set; }
    }
}
