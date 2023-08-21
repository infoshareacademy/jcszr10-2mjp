using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Data
{
    public class VacationCalendarDbContext : DbContext
    {
        public VacationCalendarDbContext(DbContextOptions<VacationCalendarDbContext> options) : base(options)
        {

        }

        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }

    }
}
