using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

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
    }
}