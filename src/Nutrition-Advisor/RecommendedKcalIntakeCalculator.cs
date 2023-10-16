namespace NutritionAdvisor
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum ActivityLevel
    {
        Sedentary = 1,
        LightlyActive,
        ModeratelyActive,
        VeryActive,
        SuperActive
    }

    public enum Goal
    {
        LoseWeight,
        GainWeight,
        BecomeFit
    }

    public class Person
    {
        public Gender Gender { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public int Age { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
    }

    public static class RecommendedCalorieIntakeCalculator
    {
        private static Dictionary<Goal, string[]> FoodRecommendations { get; set; } = InitializeFoodRecommendations();

        public static float Calculate(Person person, Goal goal)
        {
            var tdee = CalculateTdee(person);
            if (goal == Goal.LoseWeight)
            {
                return tdee - 500;
            }
            else if (goal == Goal.GainWeight)
            {
                return tdee + 250;
            }
            else
            {
                return tdee;
            }
        }

        private static float CalculateTdee(Person person)
        {
            float bmr = (person.Gender == Gender.Male) ? CalculateBmrForMen(person.Weight, person.Height, person.Age) : CalculateBmrForWomen(person.Weight, person.Height, person.Age);
            float tdee = CalculateTdee(bmr, person.ActivityLevel);
            return tdee;
        }

        // Harris-Benedict equation for men
        private static float CalculateBmrForMen(float weight, float height, int age)
        {
            return 88.362f + (13.397f * weight) + (4.799f * height) - (5.677f * age);
        }

        // Harris-Benedict equation for women
        private static float CalculateBmrForWomen(float weight, float height, int age)
        {
            return 447.593f + (9.247f * weight) + (3.098f * height) - (4.330f * age);
        }

        private static float CalculateTdee(float bmr, ActivityLevel activityLevel)
        {
            float tdee = 0;

            switch (activityLevel)
            {
                case ActivityLevel.Sedentary:
                    tdee = bmr * 1.2f;
                    break;
                case ActivityLevel.LightlyActive:
                    tdee = bmr * 1.375f;
                    break;
                case ActivityLevel.ModeratelyActive:
                    tdee = bmr * 1.55f;
                    break;
                case ActivityLevel.VeryActive:
                    tdee = bmr * 1.725f;
                    break;
                case ActivityLevel.SuperActive:
                    tdee = bmr * 1.9f;
                    break;
            }

            return tdee;
        }

        private static Dictionary<Goal, string[]> InitializeFoodRecommendations()
        {
            const string leanProteins = "Lean proteins (chicken, fish, tofu)";
            const string wholeGrains = "Whole grains (brown rice, quinoa)";
            const string fruitsAndVegetables = "Fruits and vegetables";
            const string nutsAndSeeds = "Nuts and seeds";
            const string vegetables = "Vegetables (especially leafy greens)";
            const string oatsAndQuinoa = "Whole grains (oats, quinoa)";
            const string lowFatDairy = "Low-fat dairy or dairy alternatives";
            const string complexCarbs = "Complex carbohydrates (brown rice, pasta)";
            const string healthyFats = "Healthy fats (avocado, nuts)";
            Dictionary<Goal, string[]> foodRecommendations = new Dictionary<Goal, string[]>
          {
              { Goal.BecomeFit, new string[] { leanProteins, wholeGrains, fruitsAndVegetables, nutsAndSeeds } },
              { Goal.LoseWeight, new string[] { leanProteins, vegetables, oatsAndQuinoa, lowFatDairy } },
              { Goal.GainWeight, new string[] { leanProteins, complexCarbs, healthyFats, lowFatDairy } },
              { (Goal)(-1), new string[] { "No specific recommendations for this goal" } } //default case
          };
            return foodRecommendations;
        }

        public static string[] GetFoodRecommendations(Goal goal)
        {
            if (FoodRecommendations.TryGetValue(goal, out string[] recommendations))
            {
                return recommendations;
            }
            return FoodRecommendations[(Goal)(-1)];
        }
    }
}
