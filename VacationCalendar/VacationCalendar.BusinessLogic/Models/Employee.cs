namespace VacationCalendar.BusinessLogic.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Employee(int id, string firstName, string lastName, string email)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public Employee() { }
        public override string ToString()
        {
            return $"ID: {ID}\nFirstName: {FirstName}\nLastName: {LastName}\nEmail: {Email}";
        }
        public VacationRequest Request(DateTime from, DateTime to)
        {   
            return new VacationRequest(this, from, to);
        }
    }
}
