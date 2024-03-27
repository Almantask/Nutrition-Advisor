using Newtonsoft.Json;
using NutritionAdvisor;
using Rystem.OpenAi;
using Rystem.OpenAi.Chat;
using static System.Environment;

namespace Nutrition_Advisor
{
    public class NutritionProcessorChatGpt : INutritionProcessor
    {
        private readonly IOpenAi _openAi;
        private readonly IOpenAiFactory _openAiFactory;

        public NutritionProcessorChatGpt(IOpenAi openAi, IOpenAiFactory openAiFactory)
        {
            _openAi = openAi;
            _openAiFactory = openAiFactory;
        }

        /*
         
         {
  "message": "Success",
  "recommendedFood": [
    "Salad",
    "Grilled Chicken Breast"
  ],
  "dietComparison": {
    "daily": {
      "Gyros": {
        "kcal": 500,
        "protein": 20,
        "carbohydrates": 40,
        "fat": 30,
        "sugar": 5
      },
      "French Fries": {
        "kcal": 300,
        "protein": 5,
        "carbohydrates": 30,
        "fat": 20,
        "sugar": 1
      }
    },
    "recommended": {
      "maxSugar": 10,
      "maxFat": 15,
      "maxCarbs": 50,
      "minProtein": 30,
      "maxKcal": 800
    }
  }
}
         
         */

        public async Task<NutritionResponse> Process(NutritionRequest request)
        {
            var apiSchema = File.ReadAllText(@"Resources/swagger.json");
            var requestJson = JsonConvert.SerializeObject(request, Formatting.Indented);

            var messageWithApiSchema = $"I have the following API schema{NewLine}" +
                $"```json{NewLine}{apiSchema}{NewLine}```";

            var messageWithRequestBody = $"I have this request:{NewLine}```json{requestJson}{NewLine}```{NewLine}";

            // Forgot this initially.
            var messageWithResponseSpecification = 
                "Respond only with json for this request using the schema, without using a json tag (```json nor ```)." +
                "For dietComparison.daily calculate the total kcal, carbs, protein, fat and sugar consumed. " +
                "For dietComparison.recommended calculate the recommendations based on person and their goal. Min and max values accordingly for each food property." +
                "For recommendedFood pick the best food based on person and their goal. " +
                "For message, give a tip for the person based on their goal. " +
                "Use -1 for numbers if not found";

            var openAi = _openAiFactory.Create();
            var space = NewLine;
            var result = await openAi.Chat
                .Request(new ChatMessage 
                { 
                    Role = ChatRole.User, 
                    Content = $"{messageWithApiSchema}{space}{messageWithRequestBody}{space}{space}{messageWithResponseSpecification}" 
                })
                .WithModel(ChatModelType.Gpt4)
                .ExecuteAsync();

            var firstResult = result.Choices.First();

            // Read firstResult.Message.Content with first and last lines ignored

            // Note 1: it's slow.
            // Note 2: error handling.
            // Note 3: explore calculations.
            // Note 4: specify better math instructions.

            var resposne = JsonConvert.DeserializeObject<NutritionResponse>(firstResult.Message.Content);
            // Mention this is not optimal and for an optimized solution you should build your own client that would stream the message

            return resposne;
        }
    }
}
