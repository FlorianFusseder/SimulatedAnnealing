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
            var distaceMatrix = DataReader.ReadTSP(@"bier.tsp");
            var annealing = new AnnealingBestPath(distaceMatrix);
        }
    }
}
