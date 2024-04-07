using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionAdvisor.Domain.FoodEvaluated;
using NutritionAdvisor.UseCases.Nutrition;
using Swashbuckle.AspNetCore.Filters;

namespace NutritionAdvisor.Api.Controllers
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v{version:apiVersion}/nutrition")]
    [ApiController]
    public class NutritionControllerV2 : ControllerBase
    {
        private readonly INutritionServiceV2 _nutritionService;

        public NutritionControllerV2(INutritionServiceV2 nutritionService)
        {
            _nutritionService = nutritionService;
        }

        // Provide an example of a NutritionRequest using Swashbuckle
        [HttpPost]
        public async Task<ActionResult<NutritionResponse>> GetNutritionResponse(NutritionRequest request)
        {
            var response = await _nutritionService.GetNutritionResponse(request);
            return Ok(response);
        }
    }
}
