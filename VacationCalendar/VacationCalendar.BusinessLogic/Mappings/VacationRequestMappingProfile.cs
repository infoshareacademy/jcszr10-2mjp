using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Mappings
{
    internal class VacationRequestMappingProfile : Profile
    {
        public VacationRequestMappingProfile()
        {
            CreateMap<VacationRequestDto, VacationRequest>();
        }
    }
}
