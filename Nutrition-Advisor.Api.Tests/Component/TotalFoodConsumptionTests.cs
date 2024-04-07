using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NutritionAdvisor.Domain.FoodEvaluated;
using NutritionAdvisor.Domain.FoodUnevaluated;
using NutritionAdvisor.Domain.Persona;
using System.Text;
using static NutritionAdvisor.Tests.Api.Dummy.DummyValueGenerator;

namespace NutritionAdvisor.Api.Tests.Component
{
    public class TotalFoodConsumptionTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        private readonly Mock<IFoodApiAdapter> mockFoodApiAdapter;

        public TotalFoodConsumptionTests(WebApplicationFactory<Program> factory)
        {
            mockFoodApiAdapter = new Mock<IFoodApiAdapter>();
            client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Replace the registered IFoodApiAdapter with the mock
                    services.AddScoped<IFoodApiAdapter>(_ => mockFoodApiAdapter.Object);
                });
            }).CreateClient();
        }

        [Fact]
        public async Task DailyFoodIntake_WhenSingleFood_ReturnsThatFood()
        {
            // Arrange
            var requestedFood = SetupFoodReturned("Gyros");
            var foodInRequest = new[] { new Food { Name = "Gyros", AmountG = 200 } };
            var requestBody = BuildFoodRequest(foodInRequest);

            // Act
            var response = await client.PostAsync("/api/nutrition", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Assert
            var requestedFoods = new[] { requestedFood };
            await AssertResponseEqualsExpectedFoodIntake(response, requestedFoods, foodInRequest);
        }

        [Fact]
        public async Task DailyFoodIntake_WhenMultipleFoods_ReturnsSumOfTheirValues()
        {
            // Arrange
            var requestedFood = SetupFoodReturned("Gyros");
            var requestedFood2 = SetupFoodReturned("French Fries");
            var foodInRequest = new[] { new Food { Name = "Gyros", AmountG = 200 }, new Food { Name = "French Fries", AmountG = 200 } };
            var requestBody = BuildFoodRequest(foodInRequest);

            // Act
            var response = await client.PostAsync("/api/nutrition", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Assert
            var requestedFoods = new[] { requestedFood, requestedFood2 };
            await AssertResponseEqualsExpectedFoodIntake(response, requestedFoods, foodInRequest);
        }

        [Fact]
        public async Task DailyFoodIntake_WhenRecipe_ReturnsSumOfRecipeIngredients()
        {
            // Arrange
            var requestedFood = SetupFoodReturned("Gyros");
            var requestedFood2 = SetupFoodReturned("French Fries");
            SetupRecipeReturned("Gyros with French Fries", "Gyros", "French Fries");
            var foodInRequest = new[] { new Food { Name = "Gyros with French Fries", AmountG = 100 } };
            var requestBody = BuildFoodRequest(new Food() { Name = "Gyros with French Fries", AmountG = 100 });

            // Act
            var response = await client.PostAsync("/api/nutrition", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Assert
            var requestedFoods = new[] { requestedFood, requestedFood2 };
            await AssertResponseEqualsExpectedFoodIntake(response, requestedFoods, foodInRequest);
        }

        private string BuildFoodRequest(params Food[] foods)
        {
            var requestData = new NutritionRequest
            {
                Goal = Goal.BecomeFit,
                Person = Any<Person>(),
                Food = foods
            };

            return JsonConvert.SerializeObject(requestData);
        }

        private FoodProperties SetupFoodReturned(string name)
        {
            var requestedFood = Any<FoodProperties>();
            requestedFood.Name = name;
            mockFoodApiAdapter
                .Setup(f => f.GetFoodPropertyAsync(requestedFood.Name))
                .ReturnsAsync(requestedFood);
            return requestedFood;
        }

        private async Task AssertResponseEqualsExpectedFoodIntake(HttpResponseMessage response, FoodProperties[] foodProperties, Food[] foodInRequest)
        {
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var nutritionResponse = JsonConvert.DeserializeObject<NutritionResponse>(responseContent);

            var ingredientsWithAmounts = foodInRequest.Zip(foodProperties, (fa, fp) => new FoodIntake() { Food = fp, AmountG = fa.AmountG });
            var expectedDailyFoodIntake = new DailyFoodIntake(ingredientsWithAmounts);

            expectedDailyFoodIntake.Should().BeEquivalentTo(nutritionResponse.DietComparison.Daily);
        }

        private Recipe SetupRecipeReturned(string recipeName, params string[] ingredients)
        {
            var recipe = new Recipe
            {
                Name = recipeName,
                Ingredients = ingredients.Select(i => new Food { Name = i, AmountG = 100 })
            };
            mockFoodApiAdapter
                .Setup(f => f.GetRecipeAsync(recipeName))
                .ReturnsAsync(recipe);

            return recipe;
        }
    }
}