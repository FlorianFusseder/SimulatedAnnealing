using System;
using System.Collections.Generic;

namespace SimulatedAnnealing
{
    internal class DistanceMatrix
    {

        private Dictionary<Tuple<int, int>, int> distanceMatrix;

        public DistanceMatrix(List<Tuple<int, int>> list)
        {

            this.distanceMatrix = new Dictionary<Tuple<int, int>, int>();
            double distance;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    distance = Math.Sqrt(Convert.ToInt32(Math.Pow((list[i].Item1 - list[j].Item1), 2) + Math.Pow((list[i].Item2 - list[j].Item2), 2)));
                    this.distanceMatrix.Add(new Tuple<int, int>(i, j), Convert.ToInt32(distance));
                }
            }
        }

        public int getDistance(int x, int y)
        {
            int val;
            return distanceMatrix.TryGetValue(Tuple.Create(x - 1, y - 1), out val) ? val : -1;
        }
    }
}