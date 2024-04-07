using NutritionAdvisor.Domain.Persona;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NutritionAdvisor.Api
{
    public class GoalConverter : JsonConverter<Goal>
    {
        public override Goal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object token.");
            }

            // Deserialize the JSON object
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                var root = jsonDocument.RootElement;

                // Determine which static goal instance to return based on the JSON properties
                switch (root.GetProperty("Name").GetString())
                {
                    case "Lose Weight":
                        return Goal.LoseWeight;
                    case "Gain Weight":
                        return Goal.GainWeight;
                    case "Become Fit":
                        return Goal.BecomeFit;
                    default:
                        throw new JsonException($"Unknown goal: {root.GetProperty("Name").GetString()}");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Goal value, JsonSerializerOptions options)
        {
            // This method is not needed for deserialization
            throw new NotImplementedException();
        }
    }
}

