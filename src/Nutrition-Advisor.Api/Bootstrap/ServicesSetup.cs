﻿using NutritionAdvisor.Api.Mappers;

namespace NutritionAdvisor.Api.Bootstrap
{
    public class ServicesSetup
    {
        public static void AddServices(IServiceCollection services, IConfigurationRoot configuration)
        {
             services
                    .AddDomainServices()
                    .AddControllerServices()
                    .AddOpenAiApiServices(configuration)
                    .AddControllerServices()
                    .AddDocsServices()
                    .AddHealthChecks();

            services.AddSingleton<INutritionRequestMapper, NutritionRequestMapper>();
        }
    }
}