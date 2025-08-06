using BPCalculator.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class PrivacyModelTests
{
    [Fact]
    public void OnGet_SetsLogger()
    {
        var logger = new Mock<ILogger<PrivacyModel>>();
        var model = new PrivacyModel(logger.Object);

        // Simply invoke OnGet and assert no exception
        model.OnGet();
        Assert.NotNull(model);
    }

    [Fact]
    public void OnGet_ExecutesWithoutError()
    {
        // Arrange
        var logger = new Mock<ILogger<PrivacyModel>>();
        var model = new PrivacyModel(logger.Object);

        // Act
        var exception = Record.Exception(() => model.OnGet());

        // Assert
        Assert.Null(exception);
    }
}
