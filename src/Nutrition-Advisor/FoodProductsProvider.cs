namespace NutritionAdvisor
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IFoodProductsProvider
    {
        Task<Dictionary<string, FoodProperties>> GetFoodProductsAsync(IEnumerable<string> food);
    }

    public class FoodProductsProvider : IFoodProductsProvider
    {
        private readonly IFoodApiAdapter foodApiAdapter;
        private readonly ConcurrentDictionary<string, FoodProperties> cache;

        public FoodProductsProvider(IFoodApiAdapter foodApiAdapter)
        {
            this.foodApiAdapter = foodApiAdapter ?? throw new ArgumentNullException(nameof(foodApiAdapter));
            this.cache = new ConcurrentDictionary<string, FoodProperties>();
        }

        public async Task<Dictionary<string, FoodProperties>> GetFoodProductsAsync(IEnumerable<string> food)
        {
            var tasks = food.Select(async foodItem =>
            {
                FoodProperties foodProperty;

                // Check if the food property is already in the cache
                if (cache.TryGetValue(foodItem, out foodProperty))
                {
                    return new KeyValuePair<string, FoodProperties>(foodItem, foodProperty);
                }

                // If not in the cache, check if it's a composite food
                var recipe = await foodApiAdapter.GetRecipe(foodItem);
                foodProperty = await GetFoodProperties(recipe, foodItem);
                cache.TryAdd(foodItem, foodProperty);

                return new KeyValuePair<string, FoodProperties>(foodItem, foodProperty);
            });

            var results = await Task.WhenAll(tasks);
            return results.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private FoodProperties CalculateCompositeFoodProperties(IEnumerable<FoodProperties> ingredients)
        {
            // Calculate the sum of individual ingredients
            var sumProperties = new FoodProperties();

            foreach (var ingredient in ingredients)
            {

                sumProperties.Kcal += ingredient.Kcal;
                sumProperties.Protein += ingredient.Protein;
                sumProperties.Carbohydrates += ingredient.Carbohydrates;
                sumProperties.Fat += ingredient.Fat;
            }

            return sumProperties;
        }

        private Task<FoodProperties> GetFoodProperties(Recipe recipe, string foodItem)
        {
            if (recipe == null)
            {
                return GetFoodPropertiesFor(foodItem);
            }

            return GetFoodPropertiesFor(recipe);

        }

        private async Task<FoodProperties> GetFoodPropertiesFor(string foodItem)
        {
            // If it's not a composite food, get the food property directly
            var foodProperty = await foodApiAdapter.GetFoodPropertyAsync(foodItem);
            if (foodProperty == null)
            {
                throw new Exception($"Food property {foodItem} not found");
            }

            return foodProperty;
        }

        private async Task<FoodProperties> GetFoodPropertiesFor(Recipe recipe)
        {
            var recipeIngredientFoodProperties = await GetFoodProductsAsync(recipe.Ingredients);
            var compositeFoodProperties = CalculateCompositeFoodProperties(recipeIngredientFoodProperties.Values);
            compositeFoodProperties.Name = recipe.Name;

            return compositeFoodProperties;
        }
    }

}
