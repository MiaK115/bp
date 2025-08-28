using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.AspNetCore.Hosting;

namespace BPTests
{
    public class StartupTests : IClassFixture<WebApplicationFactory<BPCalculator.Program>>
    {
        private readonly WebApplicationFactory<BPCalculator.Program> _factory;

        public StartupTests(WebApplicationFactory<BPCalculator.Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task IndexPage_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Index_Returns200_InDevelopment()
        {
            using var factory = new WebApplicationFactory<BPCalculator.Program>()
                .WithWebHostBuilder(b => b.UseEnvironment("Development"));

            var client = factory.CreateClient();
            var resp = await client.GetAsync("/");
            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        }

        [Fact]
        public async Task Index_Returns200_InNonDevelopment()
        {
            // Any env ≠ "Development" will exercise the non-dev branch in Startup.Configure
            using var factory = new WebApplicationFactory<BPCalculator.Program>()
                .WithWebHostBuilder(b => b.UseEnvironment("Staging"));

            var client = factory.CreateClient();
            var resp = await client.GetAsync("/");
            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        }

        [Fact]
        public async Task Error_Page_Resolves_InNonDevelopment()
        {
            using var factory = new WebApplicationFactory<BPCalculator.Program>()
                .WithWebHostBuilder(b => b.UseEnvironment("Production"));

            var client = factory.CreateClient();
            var resp = await client.GetAsync("/Error");
            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

            var html = await resp.Content.ReadAsStringAsync();
            // Loose assertion to avoid brittle exact text checks
            Assert.Contains("Error", html, StringComparison.OrdinalIgnoreCase);
        }
    }
}