using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Seeders;

namespace VacationCalendar.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VacationCalendarDbContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("VacationCalendar")));

            services.AddScoped<DataSeeder>();
        }
    }
}
