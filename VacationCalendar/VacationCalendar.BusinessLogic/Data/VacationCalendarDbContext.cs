using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Data
{
    public class VacationCalendarDbContext : DbContext
    {
        public VacationCalendarDbContext(DbContextOptions<VacationCalendarDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()          
               .HasOne(e => e.Manager)
               .WithMany(e => e.ManagedEmployees)
               .HasForeignKey(e => e.ManagerId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>().HasOne(e => e.Role).WithMany(r=>r.Employees).HasForeignKey(e=>e.RoleId);
        }

        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
