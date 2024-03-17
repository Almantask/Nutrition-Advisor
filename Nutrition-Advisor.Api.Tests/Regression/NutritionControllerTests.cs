﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nutrition_Advisor.Api.Tests;
using Xunit;

namespace Nutrition_Advisor.Api.Tests.Regression
{
    public class RegressionTests
    {
        [Fact]
        public async Task PostNutrition_WhenValidDataProvided_ReturnsExpectedResponse()
        {
            // Arrange
            var requestBody = @"{
                              ""Goal"": {
                                ""Name"": ""Become Fit""
                              },
                              ""Person"": {
                                ""Gender"": ""Male"",
                                ""Weight"": 85,
                                ""Height"": 1.81,
                                ""Age"": 29,
                                ""ActivityLevel"": ""ModeratelyActive""
                              },
                              ""Food"": [
                                {
                                  ""Name"": ""Gyros"",
                                  ""AmountG"": 200
                                },
                                {
                                  ""Name"": ""French Fries"",
                                  ""AmountG"": 100
                                }
                              ]
                            }";

            var expectedResponseBody = @"{
                                        ""message"": ""| Nutrient | Consumed       | Recommendation      | Difference       |\r\n|----------|----------------|---------------------|------------------|\r\n| Sugar    |       0,60g    |           38,00g    |       -37,40g    |\r\n| Fat      |      75,00g    |          415,07g    |      -340,07g    |\r\n| Protein  |      43,40g    |          127,50g    |       -84,10g    |\r\n| Carbs    |      91,00g    |          830,15g    |      -739,15g    |\r\n| Calories |    1252,00kcal |         1660,30kcal |      -408,30kcal |\r\n\nFood Recommendations:\r\n- Lean proteins (chicken, fish, tofu)\r\n- Whole grains (brown rice, quinoa)\r\n- Fruits and vegetables\r\n- Nuts and seeds\r\n"",
                                        ""recommendedFood"": [
                                            ""Lean proteins (chicken, fish, tofu)"",
                                            ""Whole grains (brown rice, quinoa)"",
                                            ""Fruits and vegetables"",
                                            ""Nuts and seeds""
                                        ],
                                        ""dietComparison"": {
                                            ""daily"": {
                                                ""name"": ""Actually Consumed"",
                                                ""kcal"": 1252,
                                                ""protein"": 43.4,
                                                ""carbohydrates"": 91,
                                                ""fat"": 75,
                                                ""sugar"": 0.6
                                            },
                                            ""recommended"": {
                                                ""maxSugar"": 38,
                                                ""maxFat"": 415.07455,
                                                ""maxCarbs"": 830.1491,
                                                ""minProtein"": 127.5,
                                                ""maxKcal"": 1660.2982
                                            }
                                        }
                                    }";

            // Act
            var response = await TestHttpClient.Instance.PostAsync("api/Nutrition", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Assert
            // Read response content as string
            string responseString = await response.Content.ReadAsStringAsync();

            // Format both jsons in the same format (idented) and compare line by line
            var expectedJson = JToken.Parse(expectedResponseBody).ToString(Formatting.Indented);
            var responseJson = JToken.Parse(responseString).ToString(Formatting.Indented);
            Assert.Equal(expectedJson, responseJson);

        }
    }
}