using System;
using System.Collections.Generic;
using System.Linq;

namespace NutritionAdvisor
{
    public record DietComparison(
        float SugarConsumed, float MaxSugarRecommendation,
        float FatConsumed, float MaxFatRecommendation, 
        float ProteinConsumed, float MinProteinRecommendation,
        float CarbsConsumed, float MaxCarbsRecommendation, 
        float KcalConsumed, float KcalRecommendation);

    public interface IFoodEvaluator
    {
        DietComparison CompareFoodConsumedToGoal(NutritionRequest request, IEnumerable<FoodProperties> foodProperties, float recommendedKcalIntake);
    }

    public class FoodEvaluator : IFoodEvaluator
    {
        public DietComparison CompareFoodConsumedToGoal(NutritionRequest request, IEnumerable<FoodProperties> foodProperties, float recommendedKcalIntake)
        {
            var food = foodProperties.Join(request.Food, fp => fp.Name, f => f.Name, (fp, f) => new { Product = fp, f.AmountG });
            float maxSugar = request.Person.Gender == Gender.Male ? Goal.MaxSugarMale : Goal.MaxSugarFemale;
            float maxFat = recommendedKcalIntake * Goal.MaxFatOfTotalKcal;
            float maxCarbs = recommendedKcalIntake * Goal.MaxCarbsOfTotalKcal;
            float minProtein = request.Person.Weight * request.Goal.MinProteinPerKg;

            return new DietComparison(
                food.Sum(f => f.Product.Sugar * f.AmountG / 100), maxSugar,
                food.Sum(f => f.Product.Fat * f.AmountG / 100), maxFat,
                food.Sum(f => f.Product.Protein * f.AmountG / 100), minProtein,
                food.Sum(f => f.Product.Carbohydrates * f.AmountG / 100), maxCarbs,
                food.Sum(f => f.Product.Kcal * f.AmountG / 100), recommendedKcalIntake);
        }
    }
}