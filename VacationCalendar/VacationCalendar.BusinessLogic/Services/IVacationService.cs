﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Dtos;

namespace VacationCalendar.BusinessLogic.Services
{
    public interface IVacationService
    {
         Task CreateVacationRequest(CreateVacationRequestDto dto);
         int CountVacationDays(DateTime dateFrom, DateTime dateTo, out string message);
    }
}