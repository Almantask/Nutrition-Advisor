using Microsoft.Extensions.Logging;
using System.Text;

namespace NutritionAdvisor
{
    public interface INutritionResponseBuilder
    {
        NutritionResponse Build(Goal goal, DietComparison dietaryComparison);
    }

    public class NutritionResponseBuilder : INutritionResponseBuilder
    {
        public NutritionResponse Build(Goal goal, DietComparison dietaryComparison)
        {
            var message = FormatResponse(goal.FoodRecommendations, dietaryComparison);

            return new NutritionResponse
            {
                DietComparison = dietaryComparison,
                Message = message,
                RecommendedFood = goal.FoodRecommendations
            };
        }

        private string FormatResponse(IEnumerable<string> foodRecommendations, DietComparison diet)
        {
            var formattedResponse = new StringBuilder();

            // Header
            formattedResponse.AppendLine("| Nutrient | Consumed       | Recommendation      | Difference       |");
            formattedResponse.AppendLine("|----------|----------------|---------------------|------------------|");

            // Sugar
            formattedResponse.AppendLine($"| Sugar    | {diet.SugarConsumed,10:F2}g    | {diet.MaxSugarRecommendation,15:F2}g    | {diet.SugarConsumed - diet.MaxSugarRecommendation,12:F2}g    |");

            // Fat
            formattedResponse.AppendLine($"| Fat      | {diet.FatConsumed,10:F2}g    | {diet.MaxFatRecommendation,15:F2}g    | {diet.FatConsumed - diet.MaxFatRecommendation,12:F2}g    |");

            // Protein
            formattedResponse.AppendLine($"| Protein  | {diet.ProteinConsumed,10:F2}g    | {diet.MinProteinRecommendation,15:F2}g    | {diet.MinProteinRecommendation - diet.ProteinConsumed,12:F2}g    |");

            // Carbs
            formattedResponse.AppendLine($"| Carbs    | {diet.CarbsConsumed,10:F2}g    | {diet.MaxCarbsRecommendation,15:F2}g    | {diet.CarbsConsumed - diet.MaxCarbsRecommendation,12:F2}g    |");

            // Calories
            formattedResponse.AppendLine($"| Calories | {diet.KcalConsumed,10:F2}kcal | {diet.KcalRecommendation,15:F2}kcal | {diet.KcalConsumed - diet.KcalRecommendation,12:F2}kcal |");

            // Food Recommendations
            formattedResponse.AppendLine("\nFood Recommendations:");
            foreach (var recommendation in foodRecommendations)
            {
                formattedResponse.AppendLine($"- {recommendation}");
            }

            return formattedResponse.ToString();
        }
    }
}