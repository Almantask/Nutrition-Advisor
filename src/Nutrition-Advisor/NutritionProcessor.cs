﻿using NutritionAdvisor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition_Advisor
{

    public class NutritionProcessor : INutritionProcessor
    {
        private readonly IFoodProductsProvider _foodProductsProvider;
        private readonly INutritionResponseBuilder _responseBuilder;
        private readonly IFoodEvaluator _foodEvaluator;

        public NutritionProcessor(
            IFoodProductsProvider foodProductsProvider,
            INutritionResponseBuilder responseBuilder,
            IFoodEvaluator foodEvaluator)
        {
            _foodProductsProvider = foodProductsProvider;
            _responseBuilder = responseBuilder;
            _foodEvaluator = foodEvaluator;
        }

        public async Task<NutritionResponse> Process(NutritionRequest request)
        {
            var foodProductsWithNutritionValue = await _foodProductsProvider.GetFoodProductsAsync(request.Food.Select(f => f.Name));
            // Categorise food products: healthy unhealthy
            var dietaryComparison = _foodEvaluator.CompareFoodConsumedToGoal(request, foodProductsWithNutritionValue.Values);

            var response = _responseBuilder.Build(request.Goal, dietaryComparison);
            return response;
        }
    }
}
