using FluentValidation.AspNetCore;
using NToastNotify;
using VacationCalendar.BusinessLogic.Extensions;
using VacationCalendar.BusinessLogic.Seeders;
using Serilog;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace VacationCalendar.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews()
                .AddFluentValidation()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            //lokalizacja aby zmieni� do adresu dopisz /?ui-culture=en-US
            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            //lokalizacja
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pl-PL")
                };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedUICultures = supportedCultures;
            });

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
                loggerConfiguration.WriteTo.File(@".\logs\vcLogs.txt").MinimumLevel.Error();
                loggerConfiguration.WriteTo.File(@"C:\VacationCalendar\logs.txt").MinimumLevel.Error();
            });

            // rejestrowanie zale�no�ci z modu�u z logik� biznesow�
            builder.Services.AddBusinessLogic(builder.Configuration);

            builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            app.UseRequestLocalization();

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