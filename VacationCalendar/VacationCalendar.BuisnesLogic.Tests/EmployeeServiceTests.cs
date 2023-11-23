﻿using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Services;
using Xunit;
using Moq;
using NToastNotify;
using VacationCalendar.BusinessLogic.Dtos;
using Microsoft.EntityFrameworkCore;
using VacationCalendar.BusinessLogic.Models;
using System.Linq.Expressions;
using System.Transactions;

namespace VacationCalendar.BuisnesLogic.Tests
{
    public class EmployeeServiceTests
    {
        private EmployeeService _sut;


        [Fact]
        public async Task CreateVacationRequest_NullEmployee_ThorwsException()
        {
            // Arrange
            List<Employee> emps = new List<Employee>();
            emps.Add(new Employee() { Email = "xxxx@xxxx.pl" });
            var mockDbSet = ServiceTestHelper.GetMockDbSet<Employee>(emps);
            var mockContext = new Mock<VacationCalendarDbContext>();
            var mockCount = new Mock<ICountVacationDaysService>();
            var mockToast = new Mock<IToastNotification>();

            //jakis setup cos ten teges?

            mockContext.Setup(c => c.Set<Employee>()).Returns(mockDbSet.Object);

            _sut = new EmployeeService(mockContext.Object, mockCount.Object, mockToast.Object);

            var dto = new CreateVacationRequestDto()
            { 
                Email = "niewiem@corobie.pl"
            };
            
            // Act


            // Assert
            await Assert.ThrowsAsync<Exception>(() => _sut.CreateVacationRequest(dto));
        }
    }
}
