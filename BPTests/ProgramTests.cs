using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using BPCalculator;

public class ProgramTests
{
    [Fact]
    public void CreateHostBuilder_ReturnsIHostBuilder()
    {
        // Act
        var hostBuilder = Program.CreateHostBuilder(new string[] { });

        // Assert
        Assert.NotNull(hostBuilder);
        Assert.IsAssignableFrom<IHostBuilder>(hostBuilder);
    }
}