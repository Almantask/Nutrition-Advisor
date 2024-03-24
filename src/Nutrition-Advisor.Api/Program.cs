using Asp.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NutritionAdvisor;
using NutritionAdvisor.Api.Controllers;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
});


builder.Services.AddScoped<NutritionProcessor>();
builder.Services.AddScoped<NutritionProcessorChatGpt>();

// TODO describe.
// Create NutritionControllerV1 with NutritionProcessor
builder.Services.AddScoped<INutritionServiceV1, NutritionServiceV1>();
builder.Services.AddScoped<INutritionServiceV2, NutritionServiceV2>();

// Create NutritionControllerV2 with NutritionProcessorChatGpt
builder.Services.AddScoped<INutritionService>(sp =>
{
    // Create NutrtionService with NutritionProcessorChatGpt
    var nutritionService = new NutritionService(
                      sp.GetRequiredService<ILogger<NutritionService>>(),
                      sp.GetRequiredService<INotificationsFacade>(),
                      sp.GetRequiredService<NotificationsConfig>(),
                      sp.GetRequiredService<NutritionProcessorChatGpt>());
    return nutritionService;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add api versioning with default version 1.0. For example baseUrl/v1.0/...
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
    .AddMvc()
    .AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


// Add swagger documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nutrition-Advisor.Api", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Nutrition-Advisor.Api", Version = "v2" });
});

builder.Services.AddSwaggerExamples();
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // swagger middleware
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nutrition-Advisor.Api v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Nutrition-Advisor.Api v2");
    });

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

public partial class Program { }