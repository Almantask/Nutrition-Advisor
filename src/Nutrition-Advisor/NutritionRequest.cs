using System.ComponentModel.DataAnnotations;

namespace NutritionAdvisor
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
