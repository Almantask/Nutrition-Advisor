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


//builder.Services.AddSwaggerGen();

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

// middleware to log every request. Use an inject logger to do logging
app.Use(async (context, next) =>
{
    // resolve logger from context
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    // log request
    logger.LogInformation($"{context.Request.Path}: sending");
    await next.Invoke();
    // log response with request path and status code
    logger.LogInformation($"{context.Request.Path} completed: {context.Response.StatusCode}");
});

app.Run();

public partial class Program {}