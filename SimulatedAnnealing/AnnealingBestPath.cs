using System;
using System.Collections.Generic;

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

        public Tour RandomTours(int repeats = 1)
        {
            for (int i = 0; i < repeats; i++)
            {
                manager.CreateRandomTour(i);
            }
            Tour t = manager.BestTour;
            return manager.BestTour;
        }

        public Tour SukzessivTours(int repeats = 1)
        {
            for (int i = 0; i < repeats; i++)
            {
                manager.GetRandomSukzessiveTour(i);
            }
            manager.SetBestAsCurrent();
            return manager.BestTour;
        }

        public Tour AllSukzessivTours()
        {
            manager.GetBestSukzessiveTour();
            manager.SetBestAsCurrent();
            return manager.BestTour;
        }

        public Tour SpecificTour(int startPosition)
        {
            manager.SukzessivFromStartLocation(124);
            manager.SetBestAsCurrent();
            return manager.BestTour;
        }

        public Tour StartAnnealing(Tour t)
        {
            //todo muss zeit runterza
            for (int i = 100000; i > 0; i--)
            {
                manager.CurrentTour = localMove(t);
                int d = manager.GetDistanceDiffernce();

                if (d <= 0)
                {
                    Console.WriteLine($"Found Improvement: {manager.CurrentTour.AbsoluteDistance} => { manager.BestTour.AbsoluteDistance}");
                    manager.SetCurrentAsBest();
                }
                else
                {
                    var e = Math.Exp(-d / i);
                    var rand = randGen.NextDouble();
                    if (e > rand)
                    {
                        Console.WriteLine($"{e} > {rand}: Take worse solution: {manager.CurrentTour.AbsoluteDistance} => { manager.BestTour.AbsoluteDistance}");
                        manager.SetCurrentAsBest();
                    }
                    else
                    {
                        Console.WriteLine($"!({e} > {rand}): Do nothing");
                    }
                    if (e != 1)
                    {
                        Console.WriteLine($"e:{e}, i:{i}, d:{d}");
                        Console.ReadLine();
                    }
                }
            }

            return t;
        }

        private Tour localMove(Tour t)
        {
            Tour tl = new Tour { List = new List<int>(t.List) };

            var pos = randGen.Next(t.List.Count);
            if(pos == 0 || pos == t.List.Count - 1 || pos == t.List.Count - 2)
                Console.WriteLine();

            if (pos == 0 || pos == t.List.Count - 1)
            {
                pos = 0;
                //Console.Write($"{tl.List[pos]} <=> {tl.List[pos+1]} to ");
                var location = tl.List[pos + 1];
                tl.List[pos + 1] = tl.List[pos];
                tl.List[pos] = location;
                tl.List[tl.List.Count - 1] = location;
                //Console.WriteLine($"{tl.List[pos]} <=> {tl.List[pos + 1]}");
            }
            else if (pos == t.List.Count - 2)
            {
                //Console.Write($"{tl.List[pos]} <=> {tl.List[1]} to ");
                var pos2 = 0;
                var location = tl.List[pos2];
                tl.List[pos2] = tl.List[pos];
                tl.List[t.List.Count - 1] = tl.List[pos];
                tl.List[pos] = location;
                //Console.WriteLine($"{tl.List[pos]} <=> {tl.List[1]}");
            }
            else
            {
                //Console.Write($"{tl.List[pos]} <=> {tl.List[pos + 1]} to ");
                var location = tl.List[pos + 1];
                tl.List[pos + 1] = tl.List[pos];
                tl.List[pos] = location;
                //Console.WriteLine($"{tl.List[pos]} <=> {tl.List[pos + 1]}");
            }

            manager.CalcTourLength(tl);

            return tl;
        }
    }
}