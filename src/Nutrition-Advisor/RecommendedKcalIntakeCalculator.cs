﻿namespace NutritionAdvisor
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
    }
}
