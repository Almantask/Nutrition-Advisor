using Microsoft.Extensions.Logging;
using Nutrition_Advisor.Domain.Notification;

namespace Nutrition_Advisor.UseCases.Nutrition
{
    public interface INutritionServiceV1 : INutritionService { }
    
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
}
