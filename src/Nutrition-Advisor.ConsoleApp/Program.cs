using Nutrition_Advisor;

var recommendedKcal = RecommendedKcalIntakeCalculator.Calculate(Gender.Male, 80, 180, 30, ActivityLevel.Sedentary, Goal.BecomeFit);
Console.WriteLine(recommendedKcal);
