using BPCalculator.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ErrorModelTests
{
    [Fact]
    public void ShowRequestId_ReturnsTrue_WhenRequestIdIsNotEmpty()
    {
        var logger = new Mock<ILogger<ErrorModel>>();
        var errorModel = new ErrorModel(logger.Object)
        {
            RequestId = "123"
        };

        Assert.True(errorModel.ShowRequestId);
    }

    [Fact]
    public void ShowRequestId_ReturnsFalse_WhenRequestIdIsNull()
    {
        var logger = new Mock<ILogger<ErrorModel>>();
        var errorModel = new ErrorModel(logger.Object)
        {
            RequestId = null
        };

        Assert.False(errorModel.ShowRequestId);
    }

    [Fact]
    public void ErrorModel_OnGet_SetsRequestId()
    {
        var logger = new Mock<ILogger<ErrorModel>>();
        var model = new ErrorModel(logger.Object);

        // Set up HttpContext via PageContext
        var httpContext = new DefaultHttpContext();
        httpContext.TraceIdentifier = "mock-trace-id";
        model.PageContext = new PageContext
        {
            HttpContext = httpContext
        };

        // Act
        model.OnGet();

        // Assert
        Assert.Equal("mock-trace-id", model.RequestId);
    }
}