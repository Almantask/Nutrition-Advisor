using Microsoft.AspNetCore.Mvc;
using Moq;
using NutritionAdvisor.UseCases.Nutrition;
using NutritionAdvisor.Api.Controllers;
using NutritionAdvisor.Domain.FoodEvaluated;

namespace NutritionAdvisor.Api.Tests
{
    public class NutritionControllerTests
    {
        [Fact]
        public async Task GetNutritionResponse_ReturnsOkObjectResult()
        {
            // Arrange
            var nutritionServiceMock = new Mock<INutritionServiceV1>();
            var nutritionController = new NutritionControllerV1(nutritionServiceMock.Object);
            var nutritionRequest = new NutritionRequest();

            // Act
            var result = await nutritionController.GetNutritionResponse(nutritionRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            nutritionServiceMock.Verify(x => x.GetNutritionResponse(nutritionRequest), Times.Once);
        }
    }
}