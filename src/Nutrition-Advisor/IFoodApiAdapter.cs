namespace NutritionAdvisor
{
    public interface IFoodApiAdapter
    {
        Task<Recipe> GetRecipe(string foodItem);
        Task<IEnumerable<string>> GetIngredientsAsync(string compositeFood);
        Task<FoodProperties> GetFoodPropertyAsync(string foodItem);
    }
}
