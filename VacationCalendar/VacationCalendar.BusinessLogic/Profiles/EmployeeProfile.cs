using AutoMapper;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<RegisterEmployeeDto, Employee>();
            //CreateMap<Employee, RegisterEmployeeDto>();
        }
    }
}
