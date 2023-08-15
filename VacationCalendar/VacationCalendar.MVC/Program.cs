using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<VacationCalendarDbContext>(option => 
                option.UseSqlServer(builder.Configuration.GetConnectionString("VacationCalendar")));

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IAdministratorService,  AdministratorService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<VacationCalendarDbContext>();
            var pandingMigrations = dbContext.Database.GetPendingMigrations();
            if (pandingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            var managers = dbContext.Managers.ToList();
            if (!managers.Any())
            {
                var manager1 = new Manager() { FirstName = "Anna", LastName = "Kowalska" };
                var manager2 = new Manager() { FirstName = "Jan", LastName = "Kowalski" };
                dbContext.Managers.AddRange(manager1, manager2);
                dbContext.SaveChanges();
            }

            var employees = dbContext.Employees.ToList();
            if (!employees.Any())
            {
                var employee1 = new Employee() { FirstName = "Magdalena", LastName = "Staniszewska", ManagerId = 1};
                var employee2 = new Employee() { FirstName = "Piotr", LastName = "Tryfon", ManagerId = 1};
                var employee3 = new Employee() { FirstName = "Jakub", LastName = "Szot", ManagerId = 2 };
                dbContext.Employees.AddRange(employee1, employee2, employee3);
                dbContext.SaveChanges();
            }

            var requestStatuses = dbContext.RequestStatuses.ToList();
            if (!requestStatuses.Any())
            {
                var status1 = new RequestStatus() { RequestStatusName = "InProgress" };
                var status2 = new RequestStatus() { RequestStatusName = "Confirmed" };
                var status3 = new RequestStatus() { RequestStatusName = "Rejected" };
                dbContext.RequestStatuses.AddRange(status1, status2, status3);  
                dbContext.SaveChanges();
            }

            var admins = dbContext.Administrators.ToList();
            if (!admins.Any())
            {
                var admin = new Administrator() { Login = "admin", Password = "password" };
                dbContext.Administrators.Add(admin);
                dbContext.SaveChanges();
            }

            app.Run();
        }
    }
}