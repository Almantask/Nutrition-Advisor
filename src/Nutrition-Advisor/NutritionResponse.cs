using NutritionAdvisor;

public class NutritionResponse
{
    public string Message { get; set; }
    public float RecommendedKcalDailyIntake { get; set; }
    public float CurrentKcalDailyIntake { get; set; }
    public IEnumerable<string> RecommendedFood { get; set; }
    public DietComparison DietComparison { get; internal set; }
}
