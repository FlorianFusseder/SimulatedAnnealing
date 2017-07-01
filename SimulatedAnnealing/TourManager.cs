using System;
using System.Linq;
using System.Collections.Generic;

namespace SimulatedAnnealing
{
    internal class TourManager
    {

        private Dictionary<Tuple<int, int>, int> distanceMatrix;
        private List<Tuple<int, int>> locationList;
        private RandomNumberGenerator randGen;
        public Tour CurrentTour { get; set; }
        public Tour BestTour { get; private set; }

        public TourManager(List<Tuple<int, int>> list)
        {
            BestTour = new Tour { AbsoluteDistance = UInt32.MaxValue };
            randGen = new RandomNumberGenerator();

            locationList = list;
            distanceMatrix = new Dictionary<Tuple<int, int>, int>();
            double distance;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    distance = Math.Sqrt(Convert.ToInt32(Math.Pow((list[i].Item1 - list[j].Item1), 2) + Math.Pow((list[i].Item2 - list[j].Item2), 2)));
                    distanceMatrix.Add(new Tuple<int, int>(i, j), (distance != 0) ? Convert.ToInt32(distance) : Int32.MaxValue);
                }
            }

        }

        internal void SetCurrentAsBest()
        {
            if (CurrentTour.List != null)
            {
                BestTour.List = new List<int>(CurrentTour.List);
                CalcTourLength(BestTour);
            }
        }

        internal void SetBestAsCurrent()
        {
            if (BestTour.List != null)
            {
                CurrentTour.List = new List<int>(BestTour.List);
                CalcTourLength(CurrentTour);
            }
        }

        public uint CalculateDistance(int x, int y)
        {
            int val;
            if (!distanceMatrix.TryGetValue(Tuple.Create(x, y), out val))
                throw new UnauthorizedAccessException($"Requested Distance is not existing {x}, {y}");
            return (uint)val;
        }

        public Tuple<int, int> GetLocationData(int index)
        {
            return locationList[index];
        }

        public void CalcTourLength(Tour t)

        {
            t.AbsoluteDistance = 0;
            for (int i = 0; i < t.List.Count - 1; i++)
            {
                t.AbsoluteDistance += CalculateDistance(t.List.ElementAt(i), t.List.ElementAt(i + 1));
            }
        }

        public void CreateRandomTour(int iteration)
        {
            CurrentTour = new Tour
            {
                List = new List<int>()
            };

            var tempList = makeList();
            int randNumber;

            for (int i = locationList.Count; tempList.Count > 0; i--)
            {
                randNumber = randGen.Next(i);
                CurrentTour.List.Add(tempList.ElementAt(randNumber));
                tempList.RemoveAt(randNumber);
            }

            CurrentTour.List.Add(CurrentTour.List[0]);

            CalcTourLength(CurrentTour);
            if (CurrentIsNewBest(iteration))
                SetCurrentAsBest();
        }

        public void GetBestSukzessiveTour()
        {
            for (int i = 0; i < locationList.Count; i++)
            {
                SukzessivFromStartLocation(i);
            }
        }

        public void GetRandomSukzessiveTour(int iteration)
        {
            int start = randGen.Next(locationList.Count - 1);
            SukzessivFromStartLocation(start);
        }

        public void SukzessivFromStartLocation(int startLocation)
        {
            CurrentTour = new Tour
            {
                List = new List<int>()
            };
            var tempList = makeList();

            int start = startLocation;
            CurrentTour.List.Add(tempList.ElementAt(start));
            tempList.RemoveAt(start);

            int end = LargestDifference(CurrentTour.List.First());
            CurrentTour.List.Add(tempList.ElementAt(end));
            tempList.RemoveAt(end);

            CurrentTour.List.Add(CurrentTour.List.ElementAt(0));

            CalcTourLength(CurrentTour);

            while (CurrentTour.List.Count < locationList.Count)
            {
                var nextLocation = GetNextBestLocation();
                FindBestOrder(nextLocation);
            }
            if (CurrentIsNewBest(startLocation))
                SetCurrentAsBest();
        }

        private void FindBestOrder(int nextLocation)
        {
            Tour tmpTour = new Tour();
            Tour bestTmpTour = new Tour { AbsoluteDistance = UInt32.MaxValue };

            for (int i = 0; i < CurrentTour.List.Count - 1; i++)
            {
                tmpTour.List = new List<int>(CurrentTour.List);

                tmpTour.List.Insert(i + 1, nextLocation);

                CalcTourLength(tmpTour);

                if (tmpTour.AbsoluteDistance < bestTmpTour.AbsoluteDistance)
                {
                    bestTmpTour.List = new List<int>(tmpTour.List);
                    CalcTourLength(bestTmpTour);
                }
            }

            CurrentTour = bestTmpTour;
            CalcTourLength(CurrentTour);
        }

        private int GetNextBestLocation()
        {
            // To get a good Tour, search for the location that has biggest, smallest distance to a location in currentTour.
            // This means:
            // Step 1.) Calculate the distance from all locations that are not in CurrentTour to every location in the tour
            // Setp 2.) Choose from all the distances the smallest one
            // step 3.) Compare the smallest distances, take the biggest one. This is the next location.
            var locationTuple = new List<Tuple<int, uint>>();


            var distances = new List<uint>();

            for (int newLocation = 0; newLocation < locationList.Count; newLocation++)
            {
                if (!CurrentTour.List.Contains(newLocation))
                {
                    foreach (var location in CurrentTour.List)
                    {
                        var distance = CalculateDistance(location, newLocation);
                        distances.Add(distance);
                    }
                    locationTuple.Add(Tuple.Create(newLocation, distances.Min()));
                    distances.Clear();
                }
            }

            return locationTuple.OrderByDescending(t => t.Item2).First().Item1;
        }

        private int LargestDifference(int v)
        {
            uint dis = 0;
            uint tmp;
            int location = Int32.MaxValue;
            for (int i = 0; i < locationList.Count; i++)
            {
                tmp = CalculateDistance(v, i);
                if (tmp > dis && tmp != Int32.MaxValue)
                {
                    //Console.WriteLine($"{i}: {dis} => {tmp}");
                    location = i;
                    dis = tmp;
                }
            }
            return location;
        }

        private bool CurrentIsNewBest(int iteration)
        {
            if (CurrentTour.AbsoluteDistance < BestTour.AbsoluteDistance)
            {
                Console.WriteLine($"Iteration {iteration}: New Best tour {BestTour.AbsoluteDistance} => {CurrentTour.AbsoluteDistance}");
                return true;
            }
            return false;
        }

        private List<int> makeList()
        {
            var tempList = new List<int>(locationList.Count);
            for (int i = 0; i < locationList.Count; i++)

            {
                tempList.Add(i);
            }
            return tempList;
        }

        public int GetDistanceDiffernce()
        {
            if((int)CurrentTour.AbsoluteDistance - (int)BestTour.AbsoluteDistance > 1000000)
                Console.WriteLine();

            return (int)CurrentTour.AbsoluteDistance - (int)BestTour.AbsoluteDistance;
        }
    }
}