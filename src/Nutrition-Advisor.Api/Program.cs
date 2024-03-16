using NutritionAdvisor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<INutritionService, NutritionService>();
builder.Services.AddScoped<INutritionResponseBuilder, NutritionResponseBuilder>();
builder.Services.AddScoped<INotificationsFacade, NotificationsFacade>();
builder.Services.AddScoped<NotificationsConfig>();
builder.Services.AddScoped<IFoodProductsProvider, FoodProductsProvider>();
builder.Services.AddScoped<IFoodEvaluator, FoodEvaluator>();
builder.Services.AddScoped<IRecommendedDailyIntakeCalculator, RecommendedDailyIntakeCalculator>();
builder.Services.AddScoped<IRecommendedKcalCalculator, RecommendedKcalCalculator>();
builder.Services.AddScoped<IEmailAdapter, EmailAPIAdapter>();
builder.Services.AddScoped<ISmsAdapter, SmsAPIAdapter>();
builder.Services.AddScoped<IFoodApiAdapter, FoodApiAdapter>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new GoalConverter());
}); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add health checks
app.MapHealthChecks("/health");

app.Run();
