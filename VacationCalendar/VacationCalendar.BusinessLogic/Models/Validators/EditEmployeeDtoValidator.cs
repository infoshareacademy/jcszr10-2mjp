using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;

namespace VacationCalendar.BusinessLogic.Models.Validators
{
    public class EditEmployeeDtoValidator : AbstractValidator<EditEmployeeDto>
    {
        public EditEmployeeDtoValidator(VacationCalendarDbContext dbContext)
        {
            RuleFor(x => x.VacaationDays).NotEmpty().GreaterThanOrEqualTo(0).LessThan(41);   
            RuleFor(dto => dto.Email)
              .NotEmpty().WithMessage("E-mail nie może być pusty.")
              .EmailAddress().WithMessage("Nieprawidłowy format adresu e-mail.");

        }
    }
}
