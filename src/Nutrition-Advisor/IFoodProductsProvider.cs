namespace NutritionAdvisor
{
    public interface IFoodProductsProvider
    {
        Task<Dictionary<string, FoodProperties>> GetFoodProductsAsync(IEnumerable<string> food);
    }
}