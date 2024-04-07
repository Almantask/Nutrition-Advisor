using System.ComponentModel.DataAnnotations;
using NutritionAdvisor.Domain.FoodUnevaluated;
using NutritionAdvisor.Domain.Persona;

namespace NutritionAdvisor.Domain.FoodEvaluated
{
    public class NutritionRequest
    {
        [Required]
        public Goal Goal { get; set; }
        [Required]
        public Person Person { get; set; }
        [Required]
        [MinLength(1)]
        public IEnumerable<Food> Food { get; set; }
    }
}
