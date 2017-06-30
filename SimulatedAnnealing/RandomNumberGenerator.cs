namespace SimulatedAnnealing
{
    internal class RandomNumberGenerator
    {
        public int Seed { get; private set; }
        public int UpperBoundary { get; set; }
        public int getNewRandomNumber
        {
            get
            {
                randNumber = (a * this.Seed + b) % this.UpperBoundary;
                return randNumber;
            }
            set { randNumber = value; }
        }
        public int getLastRandomNumber { get { return this.randNumber; } }

        private readonly int a = 1664525;
        private readonly int b = 1013904223;
        private int randNumber;

        public RandomNumberGenerator(int seed = 1, int maxValue = 100)
        {
            this.Seed = seed;
            this.UpperBoundary = maxValue;
        }

        public override string ToString()
        {
            return $"current Random Number: {randNumber}";
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

    }
}