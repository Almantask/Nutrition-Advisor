using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NutritionAdvisor;
using System.Text;

namespace Nutrition_Advisor.Api.Tests.Component
{
    public class TotalFoodConsumptionTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TotalFoodConsumptionTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DailyFoodIntake_When1Food_ReturnsThatFood()
        {
            // Arrange
            var mockFoodApiAdapter = new Mock<IFoodApiAdapter>();
            // Mock Get gyros GetFoodPropertyAsync
            var requestedFood = new FoodProperties
            {
                Name = "Gyros",
                Kcal = 470,
                Protein = 20,
                Carbohydrates = 25,
                Fat = 30,
                Sugar = 0
            };
            mockFoodApiAdapter
                .Setup(f => f.GetFoodPropertyAsync(requestedFood.Name))
                .ReturnsAsync(requestedFood);

            // Arrange
            var requestBody = @"{
                              ""Goal"": {
                                ""Name"": ""Become Fit""
                              },
                              ""Person"": {
                                ""Gender"": ""Male"",
                                ""Weight"": 85,
                                ""Height"": 1.81,
                                ""Age"": 29,
                                ""ActivityLevel"": ""ModeratelyActive""
                              },
                              ""Food"": [
                                {
                                  ""Name"": ""Gyros"",
                                  ""AmountG"": 200
                                }
                              ]
                            }";

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Replace the registered IFoodApiAdapter with the mock
                    services.AddScoped<IFoodApiAdapter>(_ => mockFoodApiAdapter.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.PostAsync("/api/nutrition", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Assert
            // Read response as NutritionResponse
            var responseContent = await response.Content.ReadAsStringAsync();


            var nutritionResponse = JsonConvert.DeserializeObject<NutritionResponse>(responseContent);
            Assert.NotNull(nutritionResponse);

            // TODO: update the chapter.
            var expectedDailyFoodIntake = new DailyFoodIntake(new List<FoodIntake>
            {
                new FoodIntake
                {
                    Food = requestedFood,
                    AmountG = 200
                },
            });

            expectedDailyFoodIntake.Should().BeEquivalentTo(nutritionResponse.DietComparison.Daily);
        }
    }
}