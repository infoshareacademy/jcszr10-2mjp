using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Extensions;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Models.Validators;
using VacationCalendar.BusinessLogic.Seeders;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews().AddFluentValidation();

            // rejestrowanie zale¿noœci z modu³u z logik¹ biznesow¹
            builder.Services.AddBusinessLogic(builder.Configuration);
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IVacationService, VacationService>();
            builder.Services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();
            builder.Services.AddScoped<IValidator<RegisterEmployeeDto>, RegisterEmployeeDtoValidator>();
            builder.Services.AddScoped<ICountVacationDaysLogic, CountVacationDaysLogic>();

            builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
            await seeder.Seed();

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

            app.Run();
        }
    }
}