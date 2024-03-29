﻿using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Email;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Models.Validators;
using VacationCalendar.BusinessLogic.Seeders;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VacationCalendarDbContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("VacationCalendar")));


            services.AddScoped<DataSeeder>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();

            services.AddScoped<IValidator<RegisterEmployeeDto>, RegisterEmployeeDtoValidator>();
            services.AddScoped<IValidator<EditEmployeeDto>, EditEmployeeDtoValidator>();
            services.AddScoped<IValidator<ChangePasswordDto>, ChangePasswordDtoValidator>();

            services.AddScoped<ICountVacationDaysService, CountVacationDaysService>();
            services.AddScoped<ICountEmployeeDaysService, CountEmployeeDaysService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();        
        }
    }
}
