namespace NutritionAdvisor
{
    public class DietComparison
    {
        public DailyFoodIntake Daily { get; set; }
        public DailyFoodIntake.Recommended Recommended { get; set; }
    }

    public interface IFoodEvaluator
    {
        DietComparison CompareFoodConsumedToGoal(NutritionRequest request, IEnumerable<FoodProperties> foodProperties);
    }

    public class FoodEvaluator : IFoodEvaluator
    {
        private readonly IRecommendedKcalCalculator _recommendedDailyKcalCalculator;
        private readonly IRecommendedDailyIntakeCalculator _recommendedDailyIntakeCalculator;

        public FoodEvaluator(IRecommendedDailyIntakeCalculator recommendedDailyIntakeCalculator, IRecommendedKcalCalculator calculator)
        {
            _recommendedDailyIntakeCalculator = recommendedDailyIntakeCalculator;
            _recommendedDailyKcalCalculator = calculator;
        }

        public DietComparison CompareFoodConsumedToGoal(NutritionRequest request, IEnumerable<FoodProperties> foodProperties)
        {
            var food = foodProperties.Join(request.Food, fp => fp.Name, f => f.Name, (fp, f) => new FoodIntake{ Food = fp, AmountG = f.AmountG });
            var daily = new DailyFoodIntake(food);
            
            var recommendedKcalIntake = _recommendedDailyKcalCalculator.CalculateRecommendedKcalIntake(request.Person, request.Goal);
            var recommended = new DailyFoodIntake.Recommended
            {
                MaxSugar = _recommendedDailyIntakeCalculator.MaxSugar(request.Person.Gender),
                MaxFat = _recommendedDailyIntakeCalculator.MaxFat(recommendedKcalIntake),
                MaxCarbs = _recommendedDailyIntakeCalculator.MaxCarbs(recommendedKcalIntake),
                MinProtein = _recommendedDailyIntakeCalculator.MinProtein(request.Person.Weight, 1.5f),
                MaxKcal = recommendedKcalIntake
            };

            return new DietComparison
           {
                Daily = daily,
                Recommended = recommended
            };
        }
    }

    public class DailyFoodIntake : FoodProperties
    {
        public DailyFoodIntake()
        {
        }

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
            public float MaxSugar { get; set; }
            public float MaxFat { get; set; }
            public float MaxCarbs { get; set; }
            public float MinProtein { get; set; }
            public float MaxKcal { get; set; }
        }
    }

    public class FoodIntake
    {
        public FoodProperties Food { get; set; }
        public float AmountG { get; set; }
    }
}