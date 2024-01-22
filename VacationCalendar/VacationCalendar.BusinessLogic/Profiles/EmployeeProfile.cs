﻿using AutoMapper;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // <From,To>
            CreateMap<RegisterEmployeeDto, Employee>();

        }
    }
}
