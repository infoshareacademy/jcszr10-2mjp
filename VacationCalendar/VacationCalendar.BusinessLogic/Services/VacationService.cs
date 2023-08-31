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
        public void ConfirmRequest(int id)
        {
           
        }
        public void RejectRequest(int id)
        {
           
        }
        [Authorize(Roles = "employee,manager")]
        public async Task CreateVacationRequest(CreateVacationRequestDto dto)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Email == dto.Email);

            if (employee == null)
            {
                throw new Exception("Nie znaleziono pracownika");
            }

            var newVacationRequest = new VacationRequest()
            {
                From = dto.From,
                To = dto.To,
                EmployeeId = employee.Id,
                VacationDays = CountVacationDays(dto.From, dto.To)
            };

            if(newVacationRequest.VacationDays > 0)
            {
                _dbContext.Add(newVacationRequest);
                //throw new Exception("Error: Testowy błąd przed zapisaniem wniosku");
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Metoda oblicza dni wakacji, pomija soboty i niedziele
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int CountVacationDays(DateTime dateFrom, DateTime dateTo, out string message)
        {
          
            if (dateFrom > dateTo)
            {
                message = "\"Data od\" nie może być nowsza od \"daty do\"!";
                return 0;
            }

            if (dateFrom.DayOfWeek == DayOfWeek.Saturday || dateFrom.DayOfWeek == DayOfWeek.Sunday
                || dateTo.DayOfWeek == DayOfWeek.Saturday || dateTo.DayOfWeek == DayOfWeek.Sunday)
            {
                message = $"Wniosek nie może zaczynać i kończyć się na sobocie lub niedzieli.";
                return 0;
            }

            if (dateFrom < DateTime.Now)
            {
                message = "Urlop nie może być planowany wstecz ani w dniu brania urlopu.";
                return 0;
            }

            if (dateFrom > DateTime.Now.AddMonths(12))
            {
                message = "Nie możesz planowac tak daleko w przyszłość.";
                return 0;
            }

            if (dateFrom == dateTo)
            {
                message = $"Wystawiono wniosek. Ilość dni urlopu:";
                return 1;
            }

            TimeSpan dateInterval = dateTo - dateFrom;
            int numberOfDays = dateInterval.Days;
            DateTime currentDay;
            int daysWithoutWeekend = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (dateFrom.AddDays(i));
                if (currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday)
                    continue;

                daysWithoutWeekend++;
            }
            message = $"Wystawiono wniosek. Ilość dni urlopu:";
            return daysWithoutWeekend;
        }
        private int CountVacationDays(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo) 
                return 0;

            if (dateFrom.DayOfWeek == DayOfWeek.Saturday || dateFrom.DayOfWeek == DayOfWeek.Sunday
                || dateTo.DayOfWeek == DayOfWeek.Saturday || dateTo.DayOfWeek == DayOfWeek.Sunday)
            {
                return 0;
            }

            if (dateFrom < DateTime.Now) 
                return 0;
            

            if (dateFrom > DateTime.Now.AddMonths(12)) 
                return 0;
            

            if (dateFrom == dateTo)
                return 1;
            

            TimeSpan dateInterval = dateTo - dateFrom;
            int numberOfDays = dateInterval.Days;
            DateTime currentDay;
            int daysWithoutWeekend = 0;
            for (int i = 0; i <= numberOfDays; i++)
            {
                currentDay = (dateFrom.AddDays(i));
                if (currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday)
                    continue;

                daysWithoutWeekend++;
            }
 
            return daysWithoutWeekend;
        }
    }
}
