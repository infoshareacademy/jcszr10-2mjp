using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
