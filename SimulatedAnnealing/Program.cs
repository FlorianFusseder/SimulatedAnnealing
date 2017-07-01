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
            //var t = annealing.randomTours(1000);
            //Console.WriteLine(t);
            var t = annealing.sukzessiveTours();
            Console.WriteLine(t);
        }
    }
}
