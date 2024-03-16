using System.Text.Json.Serialization;

namespace NutritionAdvisor
{
    public class Person
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public int Age { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ActivityLevel ActivityLevel { get; set; }
    }
}
