using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nutrition_Advisor.Domain.Food;
using Nutrition_Advisor.Domain.Intake;
using Nutrition_Advisor.Domain.Notification;
using Nutrition_Advisor.Domain.Person;
using Nutrition_Advisor.Infrastructure.Food;
using Nutrition_Advisor.Infrastructure.Notificaitons;
using Nutrition_Advisor.Infrastructure.Notificaitons.Email;
using Nutrition_Advisor.Infrastructure.Notificaitons.Sms;
using Nutrition_Advisor.UseCases.Nutrition;
using Serilog;

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

var serviceProvider = new ServiceCollection()
    .AddLogging(builder => builder.AddSerilog(serilogLogger))
    .AddSingleton<IRecommendedKcalCalculator, RecommendedKcalCalculator>()
    .AddSingleton<INutritionResponseBuilder, NutritionResponseBuilder>()
    .AddSingleton<INutritionService, NutritionService>()
    .AddSingleton<IEmailAdapter, EmailAPIAdapter>()
    .AddSingleton<ISmsAdapter, SmsAPIAdapter>()
    .AddSingleton<INotificationsFacade, NotificationsFacade>()
    .AddSingleton<NotificationsConfig>()
    .AddSingleton<IFoodApiAdapter, FoodApiAdapter>()
    .AddSingleton<IFoodProductsProvider, FoodProductsProvider>()
    .AddSingleton<IFoodEvaluator, FoodEvaluator>()
    .AddSingleton<IRecommendedDailyIntakeCalculator, RecommendedDailyIntakeCalculator>()
    .BuildServiceProvider();

var logger = serviceProvider.GetRequiredService<ILogger<NutritionService>>();
var service = serviceProvider.GetRequiredService<INutritionService>();

foreach (var goal in goals)
{
    logger.LogInformation(goal.Name);
    var request = new NutritionRequest { Food = new[] {
        new Food { Name = "Smoothie", AmountG = 500 },
        new Food { Name = "Chocolate", AmountG = 100 }}
    , Goal = goal, Person = person };
    var response = await service.GetNutritionResponse(request);
    logger.LogInformation(response.Message);
}

Console.ReadLine();
