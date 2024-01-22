using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly VacationCalendarDbContext _context;
        private readonly ICountVacationDaysService _countVacationDaysLogic;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;
        private VacationCalendarDbContext object1;
        private ICountVacationDaysService object2;
        private IToastNotification object3;

        public EmployeeService(VacationCalendarDbContext context, ICountVacationDaysService countVacationDaysLogic, IToastNotification toastNotification, IMapper mapper)
        {

            _context = context;
            _countVacationDaysLogic = countVacationDaysLogic;
            _toastNotification = toastNotification;
            _mapper = mapper;
        }

        public EmployeeService(VacationCalendarDbContext object1, ICountVacationDaysService object2, IToastNotification object3)
        {
            this.object1 = object1;
            this.object2 = object2;
            this.object3 = object3;
        }

        public async Task CreateVacationRequest(CreateVacationRequestDto dto)
        {
            var employee = await _context.Set<Employee>().FirstOrDefaultAsync(e => e.Email == dto.Email);

            if (employee == null)
            {
                throw new Exception("Nie znaleziono pracownika");
            }

            var newVacationRequest = new VacationRequest()
            {
                From = dto.From,
                To = dto.To,
                EmployeeId = employee.Id,
                VacationDays = _countVacationDaysLogic.CountVacationDays(dto.From, dto.To)
            };

            if(newVacationRequest.VacationDays < 0)
            {
                _toastNotification.AddWarningToastMessage("Wniosek przekracza dni urlopu.");
                return;
            }

            if (newVacationRequest.VacationDays > 0)
            {
                _context.Add(newVacationRequest);
                await _context.SaveChangesAsync();
            }
        }
 
        public async Task<List<VacationRequest>> GetVacationRequests()
        {
            var requests = await _context.VacationRequests.Include(req => req.Employee).Include(req => req.RequestStatus).ToListAsync();
            return requests;
        }

        public async Task<List<VacationRequest>> GetVacationRequests(string email)
        {
            var requests = await _context.VacationRequests.Where(req => req.Employee.Email == email).Include(req => req.Employee).Include(req => req.RequestStatus).ToListAsync();
            return requests;
        }

        public async Task DeleteVacationRequest(int id)
        {
            var request = await _context.VacationRequests.FindAsync(id);
            _context.VacationRequests.Remove(request);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Pomyślnie usunięto wniosek");
        }

        public void SetVacationDays(string email, int days)
        {
            var employee = _context.Employees.First(emp => emp.Email == email);
            employee.VacationDays = days;
        }

        public async Task<VacationRequest> GetVacationRequest(int id)
        {
           return await _context.VacationRequests.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task EditVacationRequest(EditVacationRequestDto dto)
        {
            var request = await _context.VacationRequests.FirstOrDefaultAsync(r => r.Id == dto.Id);
            if(request == null)
            {
                throw new Exception("Nie ma takiego wniosku!");
            }

            _mapper.Map(dto, request); // update existing 'request' with values from 'dto'

            //_mapper.Map<VacationRequest>(dto); // Create a new instance and map values

            // Without mapper
            //request.From = dto.From;
            //request.To = dto.To;
            request.VacationDays = _countVacationDaysLogic.CountVacationDays(dto.From, dto.To);

            if (request.VacationDays > 0)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}
