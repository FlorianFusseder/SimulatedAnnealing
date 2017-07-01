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
            // t1 = annealing.RandomTours(1000);
            // t1 = annealing.SukzessivTours(124);
            // t1 = annealing.AllSukzessivTours();
            t1 = annealing.SpecificTour(124);
            t2 = annealing.StartAnnealing(t1);
            Console.WriteLine(t1);
            Console.WriteLine(t2);
            
        }
    }
}
