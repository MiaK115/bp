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

        public class BloodPressurePulsePressureTests
        {
            [Theory]
            [InlineData(120, 80, 40)]
            [InlineData(140, 90, 50)]
            [InlineData(90, 60, 30)]
            public void PulsePressure_ComputesCorrectly(int systolic, int diastolic, int expected)
            {
                var bp = new BPCalculator.BloodPressure { Systolic = systolic, Diastolic = diastolic };
                Assert.Equal(expected, bp.PulsePressure);
            }

            // Keep this ONLY if Systolic/Diastolic are nullable (int?).
            [Fact]
            public void PulsePressure_HandlesNulls_WhenNullable()
            {
                // If properties are int? this compiles; delete this test if they are non-nullable int.
                var bp = new BPCalculator.BloodPressure { Systolic = null, Diastolic = 80 };
                Assert.Equal(0 - 80, bp.PulsePressure);

                bp = new BPCalculator.BloodPressure { Systolic = 120, Diastolic = null };
                Assert.Equal(120 - 0, bp.PulsePressure);
            }
        }
    }
}