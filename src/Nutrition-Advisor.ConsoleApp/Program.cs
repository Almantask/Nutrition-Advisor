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
foreach(var goal in goals)
{
    Console.WriteLine(goal.Name);
    var recommendedCalorieIntake = goal.CalculateRecommendedKcalIntake(person);
    Console.WriteLine(recommendedCalorieIntake);
    // print food recommendations
    var foodRecommendations = goal.GetFoodRecommendations();
    foreach (var foodRecommendation in foodRecommendations)
    {
        Console.WriteLine(foodRecommendation);
    }
}
