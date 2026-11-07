using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace BunkersAndBluffs.Accounts.IntegrationTests
{
    public class HealthEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        public HealthEndpointTests(WebApplicationFactory<Program> fabric)
        {
            fabricClient = fabric.CreateClient();
        }
        public HttpClient fabricClient { get; set; }

        [Fact]
        public async Task GetHealth_ReturnsOk()
        {
            var query = await fabricClient.GetAsync("/health");

            Assert.Equal(HttpStatusCode.OK, query.StatusCode);
        }
    }
}
