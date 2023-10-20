using NutritionAdvisor;

var person = new Person()
{
    Gender = Gender.Male,
    Weight = 75,
    Height = 180,
    Age = 30,
    ActivityLevel = ActivityLevel.ModeratelyActive
};

// demo goals
Goal[] goals = { Goal.GainWeight, Goal.LoseWeight, Goal.BecomeFit };
var calculator = new NutritionCalculator();
foreach(var goal in goals)
{
    Console.WriteLine(goal.Name);
    var recommendedCalorieIntake = calculator.CalculateRecommendedKcalIntake(person, goal);
    Console.WriteLine(recommendedCalorieIntake);
    // print food recommendations
    foreach (var foodRecommendation in goal.FoodRecommendations)
    {
        Console.WriteLine(foodRecommendation);
    }
}
