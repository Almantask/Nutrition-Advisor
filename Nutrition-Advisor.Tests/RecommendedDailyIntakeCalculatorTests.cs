namespace Nutrition_Advisor.Tests
{
    public class RecommendedDailyIntakeCalculatorTests
    {
        private readonly RecommendedDailyIntakeCalculator calculator;

        public RecommendedDailyIntakeCalculatorTests()
        {
            calculator = new RecommendedDailyIntakeCalculator();
        }

        [Fact]
        public void MaxFat_WhenRecommendedKcalIntakeIs1000_ShouldReturn250()
        {
            // Arrange

            var recommendedKcalIntake = 1000f;

            // Act
            var result = calculator.MaxFat(recommendedKcalIntake);

            // Assert
            var expectedMaxFat = 250f;
            Assert.Equal(expectedMaxFat, result);
        }

        [Fact]
        public void MaxFat_WhenRecommendedKcalIntakeIs1500_ShouldReturn375()
        {
            // Arrange
            var calculator = new RecommendedDailyIntakeCalculator();
            var recommendedKcalIntake = 1500f;
            var expectedMaxFat = 375f;

            // Act
            var result = calculator.MaxFat(recommendedKcalIntake);

            // Assert
            Assert.Equal(expectedMaxFat, result);
        }

        [Fact]
        public void MaxCarbs_WhenRecommendedKcalIntakeIs1000_ShouldReturn500()
        {
            // Arrange
            var recommendedKcalIntake = 1000f;

            // Act
            var result = calculator.MaxCarbs(recommendedKcalIntake);

            // Assert
            var expectedMaxCarbs = 500f;
            Assert.Equal(expectedMaxCarbs, result);
        }

        // Add more test cases as needed

        // Unit test MinProtein
        [Fact]
        public void MinProtein_WhenPersonWeightIs100AndMinProteinPerKgIs1_ShouldReturn100()
        {
            // Arrange
            var personWeight = 100f;
            var minProteinPerKg = 1f;

            // Act
            var result = calculator.MinProtein(personWeight, minProteinPerKg);

            // Assert
            var expectedMinProtein = 100f;
            Assert.Equal(expectedMinProtein, result);
        }
    }
}