using Microsoft.Extensions.Logging;

namespace NutritionAdvisor
{
    public interface INutritionService
    {
        NutritionResponse GetNutritionResponse(Goal goal, Person person);
    }

    public class NutritionService : INutritionService
    {
        private readonly ILogger<NutritionService> _logger;
        private readonly NutritionResponseBuilder _responseBuilder;
        private readonly NutritionCalculator _calculator;

        public NutritionService(ILogger<NutritionService> logger, NutritionResponseBuilder responseBuilder, NutritionCalculator calculator)
        {
            _logger = logger;
            _responseBuilder = responseBuilder;
            _calculator = calculator;
        }

        public NutritionResponse GetNutritionResponse(Goal goal, Person person)
        {
            var recommendedKcalIntake = _calculator.CalculateRecommendedKcalIntake(person, goal);
            var response = _responseBuilder.Build(goal, recommendedKcalIntake);

            _logger.LogInformation($"Nutrition response generated for goal {goal.Name}.");

            return response;
        }
    }
}
