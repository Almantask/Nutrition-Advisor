using Microsoft.Extensions.Logging;
using NutritionAdvisor;
using Serilog;
using Serilog.Extensions.Logging;

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
using var serilogLogger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt")
    .CreateLogger();
var microsoftLogger = new SerilogLoggerFactory(serilogLogger)
    .CreateLogger<NutritionCalculator>();
var calculator = new NutritionCalculator(microsoftLogger);
foreach(var goal in goals)
{
    microsoftLogger.LogInformation(goal.Name);
    var recommendedCalorieIntake = calculator.CalculateRecommendedKcalIntake(person, goal);
    microsoftLogger.LogInformation(recommendedCalorieIntake.ToString());
    // print food recommendations
    foreach (var foodRecommendation in goal.FoodRecommendations)
    {
        microsoftLogger.LogInformation(foodRecommendation);
    }
}
