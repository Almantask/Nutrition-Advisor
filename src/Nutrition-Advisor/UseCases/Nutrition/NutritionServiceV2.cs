using Microsoft.Extensions.Logging;
using Nutrition_Advisor.Domain.Notification;

namespace Nutrition_Advisor.UseCases.Nutrition
{
    public interface INutritionServiceV2 : INutritionService { }
    
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
}
