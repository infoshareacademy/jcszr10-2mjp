using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Data
{
    public class VacationCalendarDbContext : DbContext
    {
        private readonly VacationCalendarDbContext _context;
        public VacationCalendarDbContext(DbContextOptions<VacationCalendarDbContext> options, VacationCalendarDbContext context) : base(options)
        {
            _context = context;
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        "Server=(LocalDb)\\MSSQLLocalDB; Database=VacationCalendarDb;Integrated Security=True;Trusted_Connection=True;");
        //}

        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VacationRequest>(entityBuilder =>
            {
                entityBuilder.HasKey(vr => vr.Id);
                entityBuilder.HasOne(vr => vr.Employee).WithMany(e => e.VacationRequests)
                    .HasForeignKey(vr => vr.EmployeeId);
            });

            modelBuilder.Entity<Employee>(entityBuilder =>
            {
                entityBuilder.HasKey(e => e.Id);
                entityBuilder.Property(e => e.FirstName).HasColumnType("varchar(100)");
                entityBuilder.Property(e => e.LastName).HasColumnType("varchar(100)");
                entityBuilder.HasOne(e => e.Manager).WithMany(m => m.Employees).HasForeignKey(e => e.ManagerId);
            });

            modelBuilder.Entity<Manager>(entityBuilder =>
            {
                entityBuilder.Property(m => m.FirstName).HasColumnType("varchar(100)");
                entityBuilder.Property(m => m.LastName).HasColumnType("varchar(100)");
            });

        }
    }
}
