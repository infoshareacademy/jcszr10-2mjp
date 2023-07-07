using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Models;

namespace VacationCalendar.BusinessLogic.Services
{
    public static class EmployeeService
    {  
        public static Employee LogInEmployee(string firstname, string lastname, Employees employees)
        {
            var allEmployees = employees.EmployeesList;
            var employee = allEmployees
                .FirstOrDefault(e => e
                .FirstName.ToLower() == firstname.ToLower() && e.LastName.ToLower() == lastname.ToLower());
         
            return employee;
        }
    }
}
