using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedAnnealing
{
    class Program
    {
        static void Main(string[] args)
        {
            var randGen = new RandomNumberGenerator();
            var tour = DataReader.ReadTSP(@"bier.tsp");
            var annealing = new AnnealingBestPath(tour, randGen);

            Tour t1;
            Tour t2;
            t1 = annealing.RandomTours();
            // t1 = annealing.SukzessivTours();
            // t1 = annealing.AllSukzessivTours();
            // t1 = annealing.SpecificTour(5);
            // t1 = annealing.SimpleSukzessivTour(19);
            Console.WriteLine(t1);
            Tour t3 = new Tour { AbsoluteDistance = t1.AbsoluteDistance, List = new List<int>(t1.List) };

            t2 = annealing.StartAnnealing(t1);
            Console.WriteLine($"Started at: {t3}\n");
            Console.WriteLine($"Optimized: {t2}");
        }
    }
}
