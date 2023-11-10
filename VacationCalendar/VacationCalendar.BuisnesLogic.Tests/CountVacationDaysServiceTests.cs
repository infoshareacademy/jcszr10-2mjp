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
            var toastNotificationMock = new Mock<IToastNotification>();
            _sut = new CountVacationDaysService(toastNotificationMock.Object);

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
            var toastNotificationMock = new Mock<IToastNotification>();
            _sut = new CountVacationDaysService(toastNotificationMock.Object);

            // Act
            var result = _sut.IsVacationDaysAfterRequest(vacationDays, requestDays);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("2023-12-23", "2023-12-25")]
        [InlineData("2023-11-11", "2023-11-13")]
        [InlineData("2023-11-18", "2023-11-20")]
        [InlineData("2023-11-26", "2023-11-27")]
        [InlineData("2023-05-01", "2023-05-01")]
        public void VacationDaysValidation_WhenAtLeastOneDateIsHoliday_ReturnZero(DateTime from, DateTime to)
        {
            // Arrange
            var toastNotificationMock = new Mock<IToastNotification>();
            _sut = new CountVacationDaysService(toastNotificationMock.Object);

            // Act
            var result = _sut.VacationDaysValidation(from, to);

            // Assert
            Assert.Equal(0, result);
        }
    }
}