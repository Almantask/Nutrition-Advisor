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

        public async Task<NutritionResponse> Process(NutritionRequest request)
        {
            var apiSchema = File.ReadAllText(@"Resources/swagger.json");
            var requestJson = JsonConvert.SerializeObject(request, Formatting.Indented);

            var messageWithApiSchema = $"I have the following API schema{NewLine}" +
                $"```json{NewLine}{apiSchema}{NewLine}```";

            var messageWithRequestBody = $"I have this request:{NewLine}```json{requestJson}{NewLine}```{NewLine}" +
                $"Respond only with json.";

            var openAi = _openAiFactory.Create();
            var result = await openAi.Chat
                .Request(
                    new ChatMessage { Role = ChatRole.User, Content = messageWithApiSchema },
                    new ChatMessage { Role = ChatRole.User, Content = messageWithRequestBody }
                )
                .WithModel(ChatModelType.Gpt35Turbo)
                .ExecuteAsync();

            var firstResult = result.Choices.First();

            // Read firstResult.Message.Content with first and last lines ignored


            var resposne = JsonConvert.DeserializeObject<NutritionResponse>(firstResult.Message.Content);

            return resposne;
        }
    }
}
