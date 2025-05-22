using Xunit;
using Microsoft.Extensions.Logging;
using BPCalculator.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BPTests
{
    public class BloodPressureModelTests
    {
        [Fact]
        public void OnPost_WithInvalidPressure_AddsModelError()
        {
            // Arrange
            var logger = new LoggerFactory().CreateLogger<BloodPressureModel>();
            var model = new BloodPressureModel(logger)
            {
                BP = new BPCalculator.BloodPressure
                {
                    Systolic = 60,
                    Diastolic = 80
                }
            };

            // Act
            var result = model.OnPost();

            // Assert
            Assert.False(model.ModelState.IsValid);
            Assert.True(model.ModelState.ErrorCount > 0);
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public void OnPost_WithValidPressure_NoModelError()
        {
            // Arrange
            var logger = new LoggerFactory().CreateLogger<BloodPressureModel>();
            var model = new BloodPressureModel(logger)
            {
                BP = new BPCalculator.BloodPressure
                {
                    Systolic = 120,
                    Diastolic = 80
                }
            };

            // Act
            var result = model.OnPost();

            // Assert
            Assert.True(model.ModelState.IsValid);
            Assert.Equal(0, model.ModelState.ErrorCount);
            Assert.IsType<PageResult>(result);
        }
    }
}