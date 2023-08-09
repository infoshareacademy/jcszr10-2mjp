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

            var employees = dbContext.Employees.ToList();
            if (!employees.Any())
            {
                var employee1 = new Employee() { FirstName = "Magdalena", LastName = "Staniszewska" };
                var employee2 = new Employee() { FirstName = "Piotr", LastName = "Tryfon" };
                var employee3 = new Employee() { FirstName = "Jakub", LastName = "Szot" };
                dbContext.Employees.AddRange(employee1, employee2, employee3);
                dbContext.SaveChanges();
            }

            app.Run();
        }
    }
}