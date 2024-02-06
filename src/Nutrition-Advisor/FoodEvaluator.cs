using System;
using System.Collections.Generic;
using System.Linq;
using static NutritionAdvisor.DailyFoodIntake;

namespace NutritionAdvisor
{
    public class DietComparison
    {
        public DailyFoodIntake Daily { get; set; }
        public DailyFoodIntake.Recommended Recommended { get; set; }
    }

    public interface IFoodEvaluator
    {
        DietComparison CompareFoodConsumedToGoal(NutritionRequest request, IEnumerable<FoodProperties> foodProperties, float recommendedKcalIntake);
    }

    public class FoodEvaluator : IFoodEvaluator
    {
        public DietComparison CompareFoodConsumedToGoal(NutritionRequest request, IEnumerable<FoodProperties> foodProperties, float recommendedKcalIntake)
        {
            var food = foodProperties.Join(request.Food, fp => fp.Name, f => f.Name, (fp, f) => new FoodIntake{ Food = fp, AmountG = f.AmountG });
            var daily = new DailyFoodIntake(food);
            var recommended = new DailyFoodIntake.Recommended(recommendedKcalIntake, request.Person.Gender, request.Person.Weight, request.Goal.MinProteinPerKg);

            return new DietComparison
           {
                Daily = daily,
                Recommended = recommended
            };
        }
    }

    public class DailyFoodIntake : FoodProperties
    {
        public DailyFoodIntake(IEnumerable<FoodIntake> foodConsumed)
        {
            Name = "Actually Consumed";
            Sugar = foodConsumed.Sum(f => f.Food.Sugar * f.AmountG / 100);
            Fat = foodConsumed.Sum(f => f.Food.Fat * f.AmountG / 100);
            Protein = foodConsumed.Sum(f => f.Food.Protein * f.AmountG / 100);
            Carbohydrates = foodConsumed.Sum(f => f.Food.Carbohydrates * f.AmountG / 100);
            Kcal = foodConsumed.Sum(f => f.Food.Kcal * f.AmountG / 100);
        }

        public class Recommended
        {
            public float MaxSugar { get; }
            public float MaxFat { get; }
            public float MaxCarbs { get; }
            public float MinProtein { get; }

            public Recommended(float recommendedKcalIntake, Gender gender, float personWeight, float minProteinPerKg)
            {
                const float maxSugarMale = 38;
                const float maxSugarFemale = 25;
                MaxSugar = gender == Gender.Male ? maxSugarMale : maxSugarFemale;

                const float maxFatOfTotalKcal = 0.25f;
                MaxFat = recommendedKcalIntake * maxFatOfTotalKcal;

                const float maxCarbsOfTotalKcal = 0.5f;
                MaxCarbs = recommendedKcalIntake * maxCarbsOfTotalKcal;

                MinProtein = personWeight * minProteinPerKg;
            }
        }
    }

    public class FoodIntake
    {
        public FoodProperties Food { get; set; }
        public float AmountG { get; set; }
    }
}