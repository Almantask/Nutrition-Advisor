using Microsoft.Extensions.Logging;
using Nutrition_Advisor;

namespace NutritionAdvisor
{
    public interface INutritionService
    {
        NutritionResponse GetNutritionResponse(Goal goal, Person person);
    }

    public class NutritionService : INutritionService
    {
        private readonly ILogger<NutritionService> _logger;
        private readonly INutritionResponseBuilder _responseBuilder;
        private readonly INutritionCalculator _calculator;
        private readonly INotificationsFacade _notifier;
        private readonly NotificationsConfig _config;

        public NutritionService(ILogger<NutritionService> logger,
            INutritionResponseBuilder responseBuilder,
            INutritionCalculator calculator,
            INotificationsFacade notifier,
            NotificationsConfig config)
        {
            _logger = logger;
            _responseBuilder = responseBuilder;
            _calculator = calculator;
            _notifier = notifier;
            _config = config;
        }

        public NutritionResponse GetNutritionResponse(Goal goal, Person person)
        {
            var recommendedKcalIntake = _calculator.CalculateRecommendedKcalIntake(person, goal);
            var response = _responseBuilder.Build(goal, recommendedKcalIntake);

            if(_config.IsEmailEnabled)
            {
                _notifier.SendEmailNotification(response.Message, _config.Email);
            }
            else
            {
                _logger.LogInformation("Email notifications are disabled.");
            }

            if(_config.IsSmsEnabled)
            {
                _notifier.SendSmsNotification(response.Message, _config.Phone);
            }
            else
            {
                _logger.LogInformation("SMS notifications are disabled.");
            }

            _logger.LogInformation($"Nutrition response generated for goal {goal.Name}.");

            return response;
        }
    }
}
