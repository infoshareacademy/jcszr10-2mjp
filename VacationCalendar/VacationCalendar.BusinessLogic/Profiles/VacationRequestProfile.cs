using AutoMapper;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Profiles
{
    public class VacationRequestProfile : Profile
    {
        public VacationRequestProfile()
        {
            // <From,To>
            CreateMap<EditVacationRequestDto, VacationRequest>(); 
        }
    }
}
