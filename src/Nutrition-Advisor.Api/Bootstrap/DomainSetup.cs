using Nutrition_Advisor.Domain.Food;
using Nutrition_Advisor.Domain.Intake;
using Nutrition_Advisor.Domain.Notification;
using Nutrition_Advisor.Infrastructure.Food;
using Nutrition_Advisor.Infrastructure.Gpt;
using Nutrition_Advisor.Infrastructure.Notificaitons.Email;
using Nutrition_Advisor.Infrastructure.Notificaitons.Sms;
using Nutrition_Advisor.UseCases.Notification;
using Nutrition_Advisor.UseCases.Nutrition;

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
            services.AddScoped<INutritionProcessorCustom, NutritionProcessor>();
            services.AddScoped<INutritionProcessorGpt, NutritionProcessorChatGpt>();
            services.AddScoped<INutritionServiceV1, NutritionServiceV1>();
            services.AddScoped<INutritionServiceV2, NutritionServiceV2>();

            return services;
        }
    }
}
