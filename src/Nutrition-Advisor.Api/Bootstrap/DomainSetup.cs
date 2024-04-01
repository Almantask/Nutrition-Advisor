using NutritionAdvisor;

namespace Nutrition_Advisor.Api.Bootstrap
{
    public static class DomainSetup
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<INutritionResponseBuilder, NutritionResponseBuilder>();
            services.AddScoped<INotificationsFacade, NotificationsFacade>();
            services.AddScoped<NotificationsConfig>();
            services.AddScoped<IFoodProductsProvider, FoodProductsProvider>();
            services.AddScoped<IFoodEvaluator, FoodEvaluator>();
            services.AddScoped<IRecommendedDailyIntakeCalculator, RecommendedDailyIntakeCalculator>();
            services.AddScoped<IRecommendedKcalCalculator, RecommendedKcalCalculator>();
            services.AddScoped<IEmailAdapter, EmailAPIAdapter>();
            services.AddScoped<ISmsAdapter, SmsAPIAdapter>();
            services.AddScoped<IFoodApiAdapter, FoodApiAdapter>();
            services.AddScoped<NutritionProcessor>();
            services.AddScoped<NutritionProcessorChatGpt>();
            services.AddScoped<INutritionServiceV1, NutritionServiceV1>();
            services.AddScoped<INutritionServiceV2, NutritionServiceV2>();

            return services;
        }
    }
}
