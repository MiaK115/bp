using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using BPCalculator;

public class ProgramTests
{
    [Fact]
    public void CreateHostBuilder_Returns_IHostBuilder()
    {
        // Act
        var builder = Program.CreateHostBuilder(new string[] { });

        // Assert
        Assert.NotNull(builder);
        Assert.IsAssignableFrom<IHostBuilder>(builder);
    }

    [Fact]
    public async Task CreateHostBuilder_CanBuildHost()
    {
        // Just make sure building the host does not throw.
        var builder = Program.CreateHostBuilder(new string[] { });
        using var host = builder.Build();
        await host.StopAsync(); // no-op stop, ensures Build succeeded
    }
}