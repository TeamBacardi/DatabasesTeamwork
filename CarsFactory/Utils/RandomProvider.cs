using System;

namespace Utils
{
    public class RandomProvider : IRandomProvider
    {
        private Random random;

        public RandomProvider()
        {
            this.random = new Random();
        }

        public int GetRandomInRange(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
