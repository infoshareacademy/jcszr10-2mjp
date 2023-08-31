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
                if (!_dbContext.Roles.Any())
                {
                    var admin = new Role { Name = "admin" };
                    var manager = new Role { Name = "manager" };
                    var employee = new Role { Name = "employee" };
                    _dbContext.AddRange(admin, manager, employee);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Employees.Any())
                {
                    var admin = new Employee() { FirstName = "admin", LastName = "admin", Email = "admin@company.com", RoleId = 1, PasswordHash = "AQAAAAIAAYagAAAAEGSU9n8SoG2A5h6nJiAtKXqKR+tHRnKNN6q5oG/6qyF+9AzOHHPJARZ3mRPYsaB2hg==" };
                    var manager1 = new Employee() { FirstName = "Anna", LastName = "Kowalska", Email = "anna.kowalska@company.com", RoleId = 2, PasswordHash = "AQAAAAIAAYagAAAAEGSU9n8SoG2A5h6nJiAtKXqKR+tHRnKNN6q5oG/6qyF+9AzOHHPJARZ3mRPYsaB2hg==" };
                    var employee1 = new Employee() { FirstName = "Magdalena", LastName = "Staniszewska", Email = "magdalena.staniszewska@company.com", RoleId = 3 , PasswordHash = "AQAAAAIAAYagAAAAEGSU9n8SoG2A5h6nJiAtKXqKR+tHRnKNN6q5oG/6qyF+9AzOHHPJARZ3mRPYsaB2hg==" };
                    var employee2 = new Employee() { FirstName = "Piotr", LastName = "Tryfon", Email = "piotr.tryfon@company.com", RoleId = 3, PasswordHash = "AQAAAAIAAYagAAAAEGSU9n8SoG2A5h6nJiAtKXqKR+tHRnKNN6q5oG/6qyF+9AzOHHPJARZ3mRPYsaB2hg==" };
                    var employee3 = new Employee() { FirstName = "Jakub", LastName = "Szot", Email = "jakub.szot@company.com", RoleId = 3, PasswordHash = "AQAAAAIAAYagAAAAEGSU9n8SoG2A5h6nJiAtKXqKR+tHRnKNN6q5oG/6qyF+9AzOHHPJARZ3mRPYsaB2hg==" };
                    _dbContext.Employees.AddRange(admin, manager1, employee1, employee2, employee3);
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
            }
        }
    }
}
