using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Data
{
    public class VacationCalendarDbContext : DbContext
    {
        public VacationCalendarDbContext()
        {

        }
        public VacationCalendarDbContext(DbContextOptions<VacationCalendarDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany(r=>r.Employees)
                .HasForeignKey(e=>e.RoleId);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstPasswordChange)
                .HasDefaultValue(false);
        }

        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AdminSettings> AdminSettings { get; set; }

    }
}
