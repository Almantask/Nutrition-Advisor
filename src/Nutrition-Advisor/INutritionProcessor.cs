using NutritionAdvisor;

namespace Nutrition_Advisor
{
    public interface INutritionProcessor
    {
        Task<NutritionResponse> Process(NutritionRequest request);
    }
}
