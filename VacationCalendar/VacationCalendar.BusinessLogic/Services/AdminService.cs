using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
        private readonly VacationCalendarDbContext _dbContext;
        private readonly IToastNotification _toastNotification;
        public AdminService(VacationCalendarDbContext dbContext, IToastNotification toastNotification)
        {
            _dbContext = dbContext;
            _toastNotification = toastNotification;
        }

        public async Task EditSettings(int vacationDays, int roleId)
        {
            var settings = await _dbContext.AdminSettings.FirstAsync();
            if (settings == null)
            {
                throw new InvalidOperationException();
            }
            settings.DefaultVacationDays = vacationDays;
            settings.RoleId = roleId;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<AdminSettings>GetAdminSettings()
        {
            return await _dbContext.AdminSettings.FirstAsync();
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }
   
        public async Task DeleteVacationRequestAsync(int id)
        {
            var request = await _dbContext.VacationRequests.FindAsync(id);
            _dbContext.VacationRequests.Remove(request);
            _dbContext.SaveChanges();
        }

        public async Task<List<VacationRequest>> GetVacationRequestsAsync()
        {
            var requests = await _dbContext.VacationRequests.Include(req => req.Employee).Include(req => req.RequestStatus).ToListAsync();
            return requests;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return _dbContext.Employees.Include(e=>e.Role).ToList();
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }

        private async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            return employee;
        }

        public async Task<EditEmployeeDto> GetEmployeeDtoAsync(Guid id)
        {
            var employee = await GetEmployeeByIdAsync(id);
            var dto = new EditEmployeeDto()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                VacaationDays = (int)employee.VacationDays,
                RoleId = (int)employee.RoleId             
            };
            return dto;
        }
        public async Task EditEmployeeAsync(EditEmployeeDto dto)
        {
            var employee = await GetEmployeeByIdAsync(dto.Id);
            if (employee == null)
            {
                _toastNotification.AddWarningToastMessage("Nie ma takiego pracownika!");
                return;
            }

            if (employee.Email != dto.Email)
            {
                if (_dbContext.Employees.Any(e => e.Email == dto.Email))
                {
                    _toastNotification.AddWarningToastMessage("Ten Email jest już zarezerwowany!");
                    return;
                }
            }

            try
            {
                employee.Id = dto.Id;
                employee.FirstName = dto.FirstName;
                employee.LastName = dto.LastName;
                employee.Email = dto.Email;
                employee.VacationDays = dto.VacaationDays;
                employee.RoleId = dto.RoleId;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            _toastNotification.AddSuccessToastMessage("Poprawnie edytowano pracownika");
        }
    }
}
