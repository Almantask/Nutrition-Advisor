//namespace Nutrition_Advisor
//{
//    public enum Gender
//    {
//        Male,
//        Female
//    }

//    public enum ActivityLevel
//    {
//        Sedentary = 1,
//        LightlyActive,
//        ModeratelyActive,
//        VeryActive,
//        SuperActive
//    }

//    public class Person
//    {
//        public Gender gender { get; set; }
//        public float weight { get; set; }
//        public float height { get; set; }
//        public int age { get; set; }
//        public ActivityLevel activityLevel { get; set; }
//    }

//    public static class RecommendedKcalIntakeCalculator
//    {
//        public static float Calculate(Person person, Goal goal)
//        {
//            var tdee = CalculateTdee(person);
//            if (goal == Goal.LoseWeight)
//            {
//                return tdee - 500;
//            }
//            else if (goal == Goal.GainWeight)
//            {
//                return tdee + 250;
//            }
//            else
//            {
//                return tdee;
//            }
//        }

//        private static float CalculateTdee(Person person)
//        {
//            float bmr = (person.gender == Gender.Male) ? CalculateBmrForMen(person.weight, person.height, person.age) : CalculateBmrForWomen(person.weight, person.height, person.age);
//            float tdee = CalculateTdee(bmr, person.activityLevel);
//            return tdee;
//        }

//        // Harris-Benedict equation for men
//        private static float CalculateBmrForMen(float weight, float height, int age)
//        {
//            return 88.362f + (13.397f * weight) + (4.799f * height) - (5.677f * age);
//        }

//        // Harris-Benedict equation for women
//        private static float CalculateBmrForWomen(float weight, float height, int age)
//        {
//            return 447.593f + (9.247f * weight) + (3.098f * height) - (4.330f * age);
//        }

//        private static float CalculateTdee(float bmr, ActivityLevel activityLevel)
//        {
//            float tdee = 0;

//            switch (activityLevel)
//            {
//                case ActivityLevel.Sedentary:
//                    tdee = bmr * 1.2f;
//                    break;
//                case ActivityLevel.LightlyActive:
//                    tdee = bmr * 1.375f;
//                    break;
//                case ActivityLevel.ModeratelyActive:
//                    tdee = bmr * 1.55f;
//                    break;
//                case ActivityLevel.VeryActive:
//                    tdee = bmr * 1.725f;
//                    break;
//                case ActivityLevel.SuperActive:
//                    tdee = bmr * 1.9f;
//                    break;
//            }

//            return tdee;
//        }
//    }
//}
