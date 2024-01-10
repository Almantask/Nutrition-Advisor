using Microsoft.Extensions.Logging;
using Nutrition_Advisor;

namespace NutritionAdvisor
{

    public interface INutritionService
    {
        Task<NutritionResponse> GetNutritionResponse(NutritionRequest request);
    }

    public class NutritionService : INutritionService
    {
        private readonly ILogger<NutritionService> _logger;
        private readonly INutritionResponseBuilder _responseBuilder;
        private readonly INutritionCalculator _calculator;
        private readonly INotificationsFacade _notifier;
        private readonly NotificationsConfig _config;
        private readonly IFoodProductsProvider _foodProductsProvider;

        public NutritionService(ILogger<NutritionService> logger,
            INutritionResponseBuilder responseBuilder,
            INutritionCalculator calculator,
            INotificationsFacade notifier,
            NotificationsConfig config,
            IFoodProductsProvider foodProductsProvider)
        {
            _logger = logger;
            _responseBuilder = responseBuilder;
            _calculator = calculator;
            _notifier = notifier;
            _config = config;
            _foodProductsProvider = foodProductsProvider;
        }

        public async Task<NutritionResponse> GetNutritionResponse(NutritionRequest request)
        {
            var recommendedKcalIntake = _calculator.CalculateRecommendedKcalIntake(request.Person, request.Goal);
            var foodProductsWithNutritionValue = await _foodProductsProvider.GetFoodProductsAsync(request.Food.Select(f => f.Name));
            var response = _responseBuilder.Build(request.Goal, recommendedKcalIntake, foodProductsWithNutritionValue.Values, request.Food);

            SendNotification(response);

            _logger.LogInformation($"Nutrition response generated for goal {request.Goal.Name}.");

            return response;
        }

        private void SendNotification(NutritionResponse response)
        {
            if (_config.IsEmailEnabled)
            {
                _notifier.SendEmailNotificationAsync(response.Message, _config.Email);
            }
            else
            {
                _logger.LogInformation("Email notifications are disabled.");
            }

            if (_config.IsSmsEnabled)
            {
                _notifier.SendSmsNotificationAsync(response.Message, _config.Phone);
            }
            else
            {
                _logger.LogInformation("SMS notifications are disabled.");
            }
        }
    }
}
