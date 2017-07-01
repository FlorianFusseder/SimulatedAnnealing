using System;

namespace SimulatedAnnealing
{
    internal class AnnealingBestPath
    {
        private TourManager manager;
        
        private RandomNumberGenerator randGen;

        public AnnealingBestPath(TourManager distaceMatrix, RandomNumberGenerator randGen)
        {
            manager = distaceMatrix;
            this.randGen = randGen;
        }

        public Tour randomTours(int repeats = 1)
        {
            for (int i = 0; i < repeats; i++)
            {
                manager.CreateRandTour(i);
            }

            Tour t = manager.BestTour;

            return manager.BestTour;
        }

        internal Tour sukzessiveTours(int repeats =1)
        {
            for (int i = 0; i < repeats; i++)
            {
                manager.CreateSukzessiveTour(i);
            }

            Tour t = manager.BestTour;

            return manager.BestTour;
        }
    }
}