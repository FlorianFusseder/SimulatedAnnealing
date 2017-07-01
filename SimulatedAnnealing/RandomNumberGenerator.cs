using System;

namespace SimulatedAnnealing
{
    internal class RandomNumberGenerator
    {
        public int Seed { get; private set; }

        private readonly int a = 1664525;
        private readonly int b = 1013904223;

        public RandomNumberGenerator(int seed = 50)
        {
            
            this.Seed = seed;
        }

        public override string ToString()
        {
            return $"current Random Number: {Seed}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            var r = (RandomNumberGenerator)obj;

            return this.Seed == r.Seed;
        }

        public override int GetHashCode()
        {
            return this.Seed.GetHashCode();
        }

        public int Next()
        {
            Seed = (a * Seed + b) % Int32.MaxValue;
            return Seed;
        }

        public int Next(int maxVal)
        {

            if (maxVal == 0)
                return 0;
            return Math.Abs(Next() % maxVal);
        }

    }
}