using Microsoft.Extensions.Logging;

namespace NutritionAdvisor
{
    public interface INutritionResponseBuilder
    {
        NutritionResponse Build(Goal goal, float recommendedKcalIntake);
    }

    public class NutritionResponseBuilder : INutritionResponseBuilder
    {
        public NutritionResponse Build(Goal goal, float recommendedKcalIntake)
        {
            var message = FormatResponse(recommendedKcalIntake, goal.FoodRecommendations);

            return new NutritionResponse
            {
                Kcal = recommendedKcalIntake,
                Message = message,
                RecommendedFood = goal.FoodRecommendations
            };
        }

        private string FormatResponse(float recommendedKcalIntake, IEnumerable<string> foodRecommendations)
        {
            var message = $"Based on your information, we recommend a daily intake of {recommendedKcalIntake} kcal.{Environment.NewLine}" +
                          $"Here are some food recommendations: {string.Join(", ", foodRecommendations)}.";
            return message;
        }
    }
}
