namespace NutritionAdvisor.Api.Bootstrap
{
    public static class OpenAiApiSetup
    {
        public static IServiceCollection AddOpenAiApiServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var openApiKey = configuration["OpenAi:ApiKey"];
            if (string.IsNullOrEmpty(openApiKey))
            {
                throw new InvalidOperationException("OpenAi:ApiKey is required");
            }

            services.AddOpenAi(settings =>
            {
                settings.ApiKey = openApiKey;
            });

            return services;
        }
    }
}
