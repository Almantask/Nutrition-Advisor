using Microsoft.Extensions.Logging;
using NutritionAdvisor;

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
        private readonly INotificationsFacade _notifier;
        private readonly NotificationsConfig _config;
        private readonly IFoodProductsProvider _foodProductsProvider;
        private readonly IFoodEvaluator _foodEvaluator;

        public NutritionService(ILogger<NutritionService> logger,
            INutritionResponseBuilder responseBuilder,
            INotificationsFacade notifier,
            NotificationsConfig config,
            IFoodProductsProvider foodProductsProvider,
            IFoodEvaluator foodEvaluator)
        {
            _logger = logger;
            _responseBuilder = responseBuilder;
            _notifier = notifier;
            _config = config;
            _foodProductsProvider = foodProductsProvider;
            _foodEvaluator = foodEvaluator;
        }

        public async Task<NutritionResponse> GetNutritionResponse(NutritionRequest request)
        {
            var foodProductsWithNutritionValue = await _foodProductsProvider.GetFoodProductsAsync(request.Food.Select(f => f.Name));
            // Categorise food products: healthy unhealthy
            var dietaryComparison = _foodEvaluator.CompareFoodConsumedToGoal(request, foodProductsWithNutritionValue.Values);
            
            var response = _responseBuilder.Build(request.Goal, dietaryComparison);

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
