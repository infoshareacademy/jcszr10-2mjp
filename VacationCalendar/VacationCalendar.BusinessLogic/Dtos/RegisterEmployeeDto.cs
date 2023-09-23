namespace VacationCalendar.BusinessLogic.Dtos
{
    public class RegisterEmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; } = 3;
        public int VacaationDays { get; set; }
    }
}
