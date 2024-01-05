﻿namespace Nutrition_Advisor
{
    public static class ErrorSimulator
    {
        public static int ErrorRate = 50;

        public static void RunWithTransientError(Action action)
        {
            if (ShouldFail())
            {
                Console.WriteLine("Error", ConsoleColor.Red);
                throw new Exception("Transient error");
            }

            action();
        }

        private static bool ShouldFail()
        {
            return Random.Shared.Next(0, 100) < ErrorRate;
        }
    }
}