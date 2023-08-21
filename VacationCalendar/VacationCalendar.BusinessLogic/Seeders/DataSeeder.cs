using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Seeders
{
    public class DataSeeder
    {
        private readonly VacationCalendarDbContext _dbContext;

        public DataSeeder(VacationCalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            var pendingMigrations = _dbContext.Database.GetPendingMigrations();

            if (pendingMigrations.Any())
            {
                _dbContext.Database.Migrate();
            }

            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Managers.Any())
                {
                    var manager1 = new Manager() { FirstName = "Anna", LastName = "Kowalska" };
                    var manager2 = new Manager() { FirstName = "Jan", LastName = "Kowalski" };
                    _dbContext.Managers.AddRange(manager1, manager2);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Employees.Any())
                {
                    var employee1 = new Employee() { FirstName = "Magdalena", LastName = "Staniszewska", ManagerId = 1 };
                    var employee2 = new Employee() { FirstName = "Piotr", LastName = "Tryfon", ManagerId = 1 };
                    var employee3 = new Employee() { FirstName = "Jakub", LastName = "Szot", ManagerId = 2 };
                    _dbContext.Employees.AddRange(employee1, employee2, employee3);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.RequestStatuses.Any())
                {
                    var status1 = new RequestStatus() { RequestStatusName = "InProgress" };
                    var status2 = new RequestStatus() { RequestStatusName = "Confirmed" };
                    var status3 = new RequestStatus() { RequestStatusName = "Rejected" };
                    _dbContext.RequestStatuses.AddRange(status1, status2, status3);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Administrators.Any())
                {
                    var admin = new Administrator() { Login = "admin", Password = "password" };
                    _dbContext.Administrators.Add(admin);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
