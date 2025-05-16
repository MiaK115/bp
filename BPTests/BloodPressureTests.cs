using Xunit;
using BPCalculator;

namespace BPTests
{
    public class BloodPressureTests
    {
        [Theory]
        [InlineData(145, 95, BPCategory.High)]
        [InlineData(85, 55, BPCategory.Low)]
        [InlineData(130, 70, BPCategory.PreHigh)]
        [InlineData(110, 70, BPCategory.Ideal)]
        [InlineData(125, 85, BPCategory.PreHigh)]
        [InlineData(95, 50, BPCategory.Low)]
        [InlineData(140, 80, BPCategory.High)]
        [InlineData(90, 60, BPCategory.Ideal)]
        [InlineData(121, 89, BPCategory.PreHigh)]
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