namespace NutritionAdvisor
{
        public interface IFoodApiAdapter
    {
        Task<Recipe> GetRecipe(string foodItem);
        Task<IEnumerable<string>> GetIngredientsAsync(string compositeFood);
        Task<FoodProperties> GetFoodPropertyAsync(string foodItem);
    }

    // Placeholder implementation for demonstration purposes
    public class FoodApiAdapter : IFoodApiAdapter
    {
        private readonly Dictionary<string, Recipe> recipeDatabase;
        private readonly Dictionary<string, FoodProperties> foodDatabase;

        public FoodApiAdapter()
        {
            // Initialize with some dummy data
            recipeDatabase = new Dictionary<string, Recipe>
            {
                { "Smoothie", new Recipe { Name = "Smoothie", Ingredients = new List<string> { "Apple", "Banana" } } },
                // Add more recipes as needed
            };

            foodDatabase = new Dictionary<string, FoodProperties>
            {
                { "Apple", new FoodProperties { Name = "Apple", Kcal = 52, Protein = 0.3f, Carbohydrates = 14, Fat = 0.2f } },
                { "Banana", new FoodProperties { Name = "Banana", Kcal = 105, Protein = 1.3f, Carbohydrates = 27, Fat = 0.3f } },
                { "Chocolate", new FoodProperties { Name = "Chocolate", Kcal = 546, Protein = 5.3f, Carbohydrates = 58, Fat = 32.4f } },
                // Add more food items as needed
            };
        }

        public Task<Recipe> GetRecipe(string foodItem)
        {
            // Placeholder implementation, you should replace this with actual logic
            if (recipeDatabase.TryGetValue(foodItem, out var recipe))
            {
                return Task.FromResult(recipe);
            }
            else
            {
                return Task.FromResult<Recipe>(null);
            }
        }

        public Task<IEnumerable<string>> GetIngredientsAsync(string compositeFood)
        {
            // Placeholder implementation, you should replace this with actual logic
            if (recipeDatabase.TryGetValue(compositeFood, out var recipe))
            {
                return Task.FromResult<IEnumerable<string>>(recipe.Ingredients);
            }
            else
            {
                throw new InvalidOperationException($"Recipe '{compositeFood}' not found in the database.");
            }
        }

        public Task<FoodProperties> GetFoodPropertyAsync(string foodItem)
        {
            // Placeholder implementation, you should replace this with actual logic
            if (foodDatabase.TryGetValue(foodItem, out var foodProperties))
            {
                return Task.FromResult(foodProperties);
            }
            else
            {
                throw new InvalidOperationException($"Food item '{foodItem}' not found in the database.");
            }
        }
    }
}
