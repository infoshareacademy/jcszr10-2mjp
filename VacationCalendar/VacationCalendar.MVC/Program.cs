using FluentValidation.AspNetCore;
using NToastNotify;
using VacationCalendar.BusinessLogic.Extensions;
using VacationCalendar.BusinessLogic.Seeders;
using Serilog;

namespace VacationCalendar.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews().AddFluentValidation();
            builder.Services.AddMvc().AddNToastNotifyNoty(new NotyOptions
            {
                ProgressBar = true,
                Timeout = 2000,
                Layout = "topRight",
                Theme = "metroui"
            });

             builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
            {
                loggerConfiguration.WriteTo.Console();
                loggerConfiguration.WriteTo.File("./logs/vcLogs.txt").MinimumLevel.Error();
                loggerConfiguration.WriteTo.File(@"C:\VacationCalendar\logs.txt").MinimumLevel.Error();
            });

            // rejestrowanie zale¿noœci z modu³u z logik¹ biznesow¹
            builder.Services.AddBusinessLogic(builder.Configuration);

            builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            var profileAssembly = typeof(Program).Assembly;
            builder.Services.AddAutoMapper(profileAssembly);

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

            app.UseNToastNotify();

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