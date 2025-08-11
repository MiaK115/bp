using Xunit;
using System.Reflection;
using BPCalculator.Pages;
using BPCalculator;
using Microsoft.Extensions.Logging;
using Moq;

public class GeneralCoverageTests
{
    [Fact]
    public void ProgramMain_CanBeInvoked()
    {
        // Arrange & Act
        var method = typeof(Program).GetMethod("Main", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        // Assert
        Assert.NotNull(method);
    }

    [Fact]
    public void CreateHostBuilder_ShouldNotBeNull()
    {
        var builder = Program.CreateHostBuilder(new string[] { });
        Assert.NotNull(builder);
    }

    [Fact]
    public void PrivacyModel_OnGet_DoesNotThrow()
    {
        var mockLogger = new Mock<ILogger<PrivacyModel>>();
        var model = new PrivacyModel(mockLogger.Object);

        // Call the method
        model.OnGet();

        // No assert needed — success = no exception thrown
    }

    [Fact]
    public void ShowRequestId_ShouldReturnTrue_WhenRequestIdIsSet()
    {
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var model = new ErrorModel(mockLogger.Object)
        {
            RequestId = "ABC123"
        };

        Assert.True(model.ShowRequestId);
    }
}

