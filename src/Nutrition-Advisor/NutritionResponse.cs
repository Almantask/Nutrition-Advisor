public class NutritionResponse
{
    public string Message { get; set; }
    public float Kcal { get; set; }
    public IEnumerable<string> RecommendedFood { get; set; }
}
