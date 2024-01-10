using Microsoft.Extensions.Logging;

namespace NutritionAdvisor
{
    public interface INutritionResponseBuilder
    {
        NutritionResponse Build(Goal goal, float recommendedKcalIntake, IEnumerable<FoodProperties> foodProperties, IEnumerable<Food> foodAmounts);
    }

    public class NutritionResponseBuilder : INutritionResponseBuilder
    {
        public NutritionResponse Build(Goal goal, float recommendedKcalIntake, IEnumerable<FoodProperties> foodProperties, IEnumerable<Food> foodAmounts)
        {
            var food = foodProperties.Join(foodAmounts, fp => fp.Name, f => f.Name, (fp, f) => new { fp.Kcal, f.AmountG });
            var currentKcalIntake = food.Sum(f => f.Kcal * f.AmountG / 100);
            var message = FormatResponse(recommendedKcalIntake, goal.FoodRecommendations, currentKcalIntake);

            return new NutritionResponse
            {
                RecommendedKcalDailyIntake = recommendedKcalIntake,
                CurrentKcalDailyIntake = currentKcalIntake,
                Message = message,
                RecommendedFood = goal.FoodRecommendations
            };
        }

        private string FormatResponse(float recommendedKcalIntake, IEnumerable<string> foodRecommendations, float currentKcalIntake)
        {
            var message = $"Based on your information, we recommend a daily intake of {recommendedKcalIntake} kcal.{Environment.NewLine}" +
                          $"Your current daily intake is {currentKcalIntake} kcal.{Environment.NewLine}" +
                          $"Here are some food recommendations: {string.Join(", ", foodRecommendations)}.";
            return message;
        }
    }
}