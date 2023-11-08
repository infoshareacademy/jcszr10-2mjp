using Moq;
using NToastNotify;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.BuisnesLogic.Tests
{
    public class CuntVacationDaysServiceTests
    {    
        private CountVacationDaysService _sut;

        [Theory]
        [InlineData(1, 10)]
        [InlineData(0, 1)]
        [InlineData(10, 11)]
        public void IsVacationDaysAfterRequest_WhenRequestDaysAreGreatherThenVacationDays_ReturnFalse(int vacationDays, int requestDays)
        {
            // Arrange
            var toastNotification = new Mock<IToastNotification>();
            _sut = new CountVacationDaysService(toastNotification.Object);

            // Act
            var result = _sut.IsVacationDaysAfterRequest(vacationDays, requestDays);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(10, 1)]
        [InlineData(11, 10)]
        public void IsVacationDaysAfterRequest_WhenVacationDaysAreGreatherThenRequestDays_ReturnTrue(int vacationDays, int requestDays)
        {
            // Arrange
            var toastNotification = new Mock<IToastNotification>();
            _sut = new CountVacationDaysService(toastNotification.Object);

            // Act
            var result = _sut.IsVacationDaysAfterRequest(vacationDays, requestDays);

            // Assert
            Assert.True(result);
        }
    }
}