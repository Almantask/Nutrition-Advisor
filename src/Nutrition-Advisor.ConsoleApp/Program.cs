using NutritionAdvisor;

var person = new Person()
{
    Gender = Gender.Male,
    Weight = 75,
    Height = 180,
    Age = 30,
    ActivityLevel = ActivityLevel.ModeratelyActive
};

var goal = Goal.LoseWeight;

var recommendedCalorieIntake = RecommendedCalorieIntakeCalculator.Calculate(person, goal);
Console.WriteLine(recommendedCalorieIntake);

// print food recommendations
var foodRecommendations = RecommendedCalorieIntakeCalculator.GetFoodRecommendations(goal);
foreach (var foodRecommendation in foodRecommendations)
{
    Console.WriteLine(foodRecommendation);
}
