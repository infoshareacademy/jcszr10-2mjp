using FluentValidation;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;

namespace VacationCalendar.BusinessLogic.Models.Validators
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator(VacationCalendarDbContext dbContext)
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Nie może być krótsze niz 4");
            RuleFor(x => x.NewPassword).MinimumLength(4).WithMessage("Nie może być krótsze niz 4").NotEqual(e => e.OldPassword).WithMessage("Nowe hasło nie może być takie samo jak stare hasło");
            RuleFor(x => x.ConfirmPassword).Equal(e => e.NewPassword);         
        }
    }
}
