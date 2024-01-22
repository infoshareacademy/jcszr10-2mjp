using AutoMapper;
using Moq;
using NToastNotify;
using VacationCalendar.BusinessLogic.Data;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.BuisnesLogic.Tests
{
    public class EmployeeServiceTests
    {
        private EmployeeService _sut;

        [Fact]
        public async Task CreateVacationRequest_NullEmployee_ThorwsException()
        {
            // Arrange
            List<Employee> emps = new List<Employee>
            {
                new Employee() { Email = "xxxx@xxxx.pl" }
            };
            var mockDbSet = ServiceTestHelper.GetMockDbSet<Employee>(emps);
            var mockContext = new Mock<VacationCalendarDbContext>();
            var mockCount = new Mock<ICountVacationDaysService>();
            var mockToast = new Mock<IToastNotification>();
            var mockAutoMapper = new Mock<IMapper>();

            mockContext.Setup(c => c.Set<Employee>()).Returns(mockDbSet.Object);

            _sut = new EmployeeService(mockContext.Object, mockCount.Object, mockToast.Object, mockAutoMapper.Object);

            // Act
            var dto = new CreateVacationRequestDto()
            {
                Email = "niewiem@corobie.pl"
            };

            // Assert
            await Assert.ThrowsAsync<Exception>(() => _sut.CreateVacationRequest(dto));
        }
    }
}
