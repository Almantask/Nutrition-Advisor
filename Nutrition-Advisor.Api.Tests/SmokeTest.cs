using System.Net;

namespace Nutrition_Advisor.Api.Tests
{
    public class SmokeTest
    {
        // Test whether /health endpoint returns 200 OK. Base uri is https://localhost:7230
        [Fact]
        public async Task HealthEndpoint_ReturnsOk()
        {
            // Arrange
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7230/health");

            // Act
            var response = await client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
