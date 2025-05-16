using Xunit;
using BPCalculator;

namespace BPTests
{
    public class BloodPressureTests
    {
        [Theory]
        [InlineData(150, 95, BPCategory.High)]
        [InlineData(85, 55, BPCategory.Low)]
        [InlineData(130, 85, BPCategory.PreHigh)]
        [InlineData(110, 70, BPCategory.Ideal)]
        public void Category_ReturnsExpectedCategory(int systolic, int diastolic, BPCategory expected)
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = systolic,
                Diastolic = diastolic
            };

            // Act
            var result = bp.Category;

            // Assert
            Assert.Equal(expected, result);
        }
    }
}