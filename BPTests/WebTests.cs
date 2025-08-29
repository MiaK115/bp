using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class WebTests : IClassFixture<WebApplicationFactory<BPCalculator.Startup>>
{
    private readonly WebApplicationFactory<BPCalculator.Startup> _factory;
    public WebTests(WebApplicationFactory<BPCalculator.Startup> factory) => _factory = factory;

    [Fact]
    public async Task Index_RendersFormFields()
    {
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        Assert.Contains("name=\"BP.Systolic\"", html);
        Assert.Contains("name=\"BP.Diastolic\"", html);
    }
}