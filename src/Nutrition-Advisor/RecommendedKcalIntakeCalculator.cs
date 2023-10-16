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

    public class Person
    {
        public Gender Gender { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public int Age { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
    }

    public class Goal
    {
        public static readonly Goal LoseWeight = new Goal("Lose Weight", -500, new string[]
        {
                    "Lean proteins (chicken, fish, tofu)",
                    "Vegetables (especially leafy greens)",
                    "Oats and quinoa",
                    "Low-fat dairy or dairy alternatives"
        });

        public static readonly Goal GainWeight = new Goal("Gain Weight", 250, new string[]
        {
            "Lean proteins (chicken, fish, tofu)",
            "Complex carbohydrates (brown rice, pasta)",
            "Healthy fats (avocado, nuts)",
            "Low-fat dairy or dairy alternatives"
        });

        public static readonly Goal BecomeFit = new Goal("Become Fit", 0, new string[]
        {
            "Lean proteins (chicken, fish, tofu)",
            "Whole grains (brown rice, quinoa)",
            "Fruits and vegetables",
            "Nuts and seeds"
        });

        public string Name { get; }
        public int RecommendedKcalAdjustment { get; }
        public string[] FoodRecommendations { get; }

        private Goal(string name, int recommendedKcalAdjustment, string[] foodRecommendations)
        {
            Name = name;
            RecommendedKcalAdjustment = recommendedKcalAdjustment;
            FoodRecommendations = foodRecommendations;
        }

        public float CalculateRecommendedKcalIntake(Person person)
        {
            var tdee = CalculateTdee(person);
            return tdee + RecommendedKcalAdjustment;
        }

        public string[] GetFoodRecommendations()
        {
            return FoodRecommendations;
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
    }
}
