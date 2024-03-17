// Unit test for NutritionController
using Microsoft.AspNetCore.Mvc;
using Moq;
using NutritionAdvisor.Api.Controllers;

namespace NutritionAdvisor.Api.Tests
{
    public class NutritionControllerTests
    {
        [Fact]
        public async Task GetNutritionResponse_ReturnsOkObjectResult()
        {
            // Arrange
            var nutritionServiceMock = new Mock<INutritionService>();
            var nutritionController = new NutritionController(nutritionServiceMock.Object);
            var nutritionRequest = new NutritionRequest();

            // Act
            var result = await nutritionController.GetNutritionResponse(nutritionRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            // verify that nutritionServiceMock was called
            nutritionServiceMock.Verify(x => x.GetNutritionResponse(nutritionRequest), Times.Once);
        }
    }
}