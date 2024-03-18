using Microsoft.AspNetCore.Mvc;

namespace NutritionAdvisor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionController : ControllerBase
    {
        private readonly INutritionService _nutritionService;

        public NutritionController(INutritionService nutritionService)
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
