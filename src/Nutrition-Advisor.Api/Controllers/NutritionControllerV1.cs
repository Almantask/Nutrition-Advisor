using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace NutritionAdvisor.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/nutrition")]
    [ApiController]
    public class NutritionControllerV1 : ControllerBase
    {
        private readonly INutritionServiceV1 _nutritionService;

        public NutritionControllerV1(INutritionServiceV1 nutritionService)
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
