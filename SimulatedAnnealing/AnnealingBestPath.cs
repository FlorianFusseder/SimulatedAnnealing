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

        public Tour SimpleSukzessivTour(int start)
        {
            manager.SimpleSukzessivTour(start);
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
            manager.SukzessivFromStartLocation(startPosition);
            manager.SetBestAsCurrent();
            return manager.BestTour;
        }

        public Tour StartAnnealing(Tour toEvaluate)
        {

            Tour overallBest = new Tour
            {
                AbsoluteDistance = toEvaluate.AbsoluteDistance,
                List = new List<int>(toEvaluate.List)
            };

            Console.WriteLine($"Start:\n{toEvaluate}\n");
            

            for (int i = 1000000; i > 0; i--)
            {
                manager.CurrentTour = localMove(toEvaluate);
                int d = manager.GetDistanceDiffernce();
                double time = i / 1000;
                if (d <= 0)
                {
                    //Console.WriteLine($"Found Improvement: {manager.CurrentTour.AbsoluteDistance} => { manager.BestTour.AbsoluteDistance}");
                    manager.SetCurrentAsBest();
                    if (manager.BestTour.AbsoluteDistance < overallBest.AbsoluteDistance)
                    {
                        overallBest.AbsoluteDistance = manager.BestTour.AbsoluteDistance;
                        overallBest.List = new List<int>(manager.BestTour.List);
                        Console.Write("|");
                        //Console.WriteLine($"New overall Best: {overallBest}");
                    }
                }
                else
                {
                    var e = Math.Exp(-d / time);
                    var rand = randGen.NextDouble();

                    if (e > rand)
                    {
                        manager.SetCurrentAsBest();
                    }
                }
                if(i % 10000 == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Iterations left {i}");
                    Console.WriteLine($"Overall best: {overallBest.AbsoluteDistance}");
                    Console.WriteLine($"Current: {manager.CurrentTour.AbsoluteDistance}");
                    Console.WriteLine($"Local best: {manager.BestTour.AbsoluteDistance}\n\n");
                }
            }
            return overallBest;
        }

        private Tour localMove(Tour t)
        {
            Tour tl = new Tour { List = new List<int>(t.List) };

            var pos = randGen.Next(t.List.Count);

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