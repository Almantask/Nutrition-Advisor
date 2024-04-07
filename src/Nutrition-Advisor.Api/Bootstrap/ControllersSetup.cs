using NutritionAdvisor;

namespace NutritionAdvisor.Api.Bootstrap
{
    public static class ControllersSetup
    {
        public static IServiceCollection AddControllerServices(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new GoalConverter());
            });

            return services;
        }
    }
}
