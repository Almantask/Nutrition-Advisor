namespace NutritionAdvisor
{
    public class NutritionRequest
    {
        public Goal Goal { get; set; }
        public Person Person { get; set; }
        public IEnumerable<Food> Food { get; set; }
    }
}
