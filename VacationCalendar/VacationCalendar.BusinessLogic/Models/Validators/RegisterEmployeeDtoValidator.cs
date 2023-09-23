using FluentValidation;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;

namespace VacationCalendar.BusinessLogic.Models.Validators
{
    public class RegisterEmployeeDtoValidator : AbstractValidator<RegisterEmployeeDto>
    {
        public RegisterEmployeeDtoValidator(VacationCalendarDbContext dbContext)
        {
            RuleFor(x => x.VacaationDays).NotEmpty().GreaterThanOrEqualTo(0).LessThan(41);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(4);
            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Employees.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "Ten email jest już zarezerwowany");
                }
            });
        }
    }
}
