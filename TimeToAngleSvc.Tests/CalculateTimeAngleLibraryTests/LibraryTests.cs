using TimeToAngleSvc.Library.Implementations;
using TimeToAngleSvc.Models;

namespace TimeToAngleSvc.Tests.CalculateTimeAngleLibraryTests
{
    public class LibraryTests
    {
        private readonly CalculateTimeAngleLibrary _library = new();

        [Theory]
        [InlineData("3", "0", 90)]
        [InlineData("12", "0", 0)]
        [InlineData("6", "0", 180)]
        [InlineData("9", "0", 90)]
        [InlineData("3", "15", 7.5)]
        [InlineData("0", "0", 0)]
        [InlineData("23", "59", -5.5)]
        public async Task CalculateAngleAsync_ValidInput_ReturnsCorrectAngle(string hour, string minute, double expectedAngle)
        {
            var request = new TimeToAngleRequest { Hour = hour, Minute = minute };
            var result = await _library.CalculateAngleAsync(request);

            Assert.True(result.Success);
            Assert.NotNull(result.Message);
            Assert.Equal(expectedAngle, result.Angle, 1);
        }

        [Theory]
        [InlineData("abc", "0")]
        [InlineData("3", "xyz")]
        [InlineData("", "15")]
        [InlineData("12", "")]
        public async Task CalculateAngleAsync_InvalidFormat_ReturnsError(string hour, string minute)
        {
            var request = new TimeToAngleRequest { Hour = hour, Minute = minute };
            var result = await _library.CalculateAngleAsync(request);

            Assert.False(result.Success);
            Assert.NotNull(result.Message);
            Assert.Equal(0, result.Angle);
        }

        [Theory]
        [InlineData("-1", "0")]
        [InlineData("24", "0")]
        [InlineData("12", "-5")]
        [InlineData("12", "60")]
        public async Task CalculateAngleAsync_OutOfRange_ReturnsError(string hour, string minute)
        {
            var request = new TimeToAngleRequest { Hour = hour, Minute = minute };
            var result = await _library.CalculateAngleAsync(request);

            Assert.False(result.Success);
            Assert.NotNull(result.Message);
            Assert.Equal(0, result.Angle);
        }
    }
}