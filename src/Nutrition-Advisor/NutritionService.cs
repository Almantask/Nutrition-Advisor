using Microsoft.Extensions.Logging;
using Nutrition_Advisor;
using NutritionAdvisor;

namespace NutritionAdvisor
{
    public interface INutritionServiceV1 : INutritionService { }
    public interface INutritionServiceV2 : INutritionService { }

    public class NutritionServiceV1 : NutritionService, INutritionServiceV1
    {
        public NutritionServiceV1(
            ILogger<NutritionService> logger,
            INotificationsFacade notifier,
            NotificationsConfig config,
            NutritionProcessor processor)
            : base(logger, notifier, config, processor)
        {
        }
    }

    public class NutritionServiceV2 : NutritionService, INutritionServiceV2
    {
        public NutritionServiceV2(
            ILogger<NutritionService> logger,
            INotificationsFacade notifier,
            NotificationsConfig config,
            NutritionProcessorChatGpt processor)
            : base(logger, notifier, config, processor)
        {
        }
    }

    public interface INutritionService
    {
        Task<NutritionResponse> GetNutritionResponse(NutritionRequest request);
    }

    public class NutritionService : INutritionService
    {
        private readonly ILogger<NutritionService> _logger;
        private readonly INotificationsFacade _notifier;
        private readonly NotificationsConfig _config;
        private readonly INutritionProcessor _processor;

        public NutritionService(
                       ILogger<NutritionService> logger,
                       INotificationsFacade notifier,
                       NotificationsConfig config,
                       INutritionProcessor processor)
        {
            _logger = logger;
            _notifier = notifier;
            _config = config;
            _processor = processor;
        }

        public async Task<NutritionResponse> GetNutritionResponse(NutritionRequest request)
        {
            NutritionResponse response = await _processor.Process(request);

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
