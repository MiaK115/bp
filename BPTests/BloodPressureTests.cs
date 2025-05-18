using Xunit;
using BPCalculator;

namespace BPTests
{
    public class BloodPressureTests
    {
        [Theory]
        [InlineData(70, 40, BPCategory.Low)]
        [InlineData(89, 59, BPCategory.Low)]
        [InlineData(90, 55, BPCategory.Ideal)]
        [InlineData(120, 55, BPCategory.Ideal)]
        [InlineData(90, 60, BPCategory.Ideal)]
        [InlineData(120, 60, BPCategory.Ideal)]
        [InlineData(90, 80, BPCategory.Ideal)]
        [InlineData(120, 80, BPCategory.Ideal)]
        [InlineData(90, 81, BPCategory.PreHigh)]
        [InlineData(120, 81, BPCategory.PreHigh)]
        [InlineData(120, 89, BPCategory.PreHigh)]
        [InlineData(120, 90, BPCategory.High)]
        [InlineData(121, 60, BPCategory.PreHigh)]
        [InlineData(139, 60, BPCategory.PreHigh)]
        [InlineData(121, 89, BPCategory.PreHigh)]
        [InlineData(139, 89, BPCategory.PreHigh)]
        [InlineData(121, 90, BPCategory.High)]
        [InlineData(139, 90, BPCategory.High)]
        [InlineData(140, 60, BPCategory.High)]
        [InlineData(140, 95, BPCategory.High)]
        [InlineData(190, 100, BPCategory.High)]
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