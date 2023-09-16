using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class VacationService : IVacationService
    {
        private readonly VacationCalendarDbContext _dbContext;

        public VacationService(VacationCalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
