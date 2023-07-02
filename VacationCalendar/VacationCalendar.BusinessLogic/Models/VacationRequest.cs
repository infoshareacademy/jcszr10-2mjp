namespace VacationCalendar.BusinessLogic.Models
{
    public class VacationRequest
    {
        private static int nextID = 1;
        public int ID { get; set; }
        public Employee Emp { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Status { get; set; }

        public VacationRequest(Employee emp, DateTime from, DateTime to)
        {
            ID = nextID;
            nextID++;
            Emp = emp;
            From = from;
            To = to;
            Status = true;
        }
        public override string ToString()
        {
            return $"ID: {ID}\nEmployee: \n{Emp.ToString()}\nFrom: {From}\nTo: {To}\nVacationDays: {VacationDays()}\nStatus: {Status}";
        }

        public int VacationDays()
        {
            TimeSpan dateInterval = To - From;
            int numberOfDays = dateInterval.Days;
            DateTime currentDay;
            int daysWithoutWeekend = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (From.AddDays(i));
                if (currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday)
                    continue;

                daysWithoutWeekend++;
            }
            return daysWithoutWeekend;
        }
    }
}
