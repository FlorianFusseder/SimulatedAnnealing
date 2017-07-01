using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedAnnealing
{
    class Tour
    {
        public uint AbsoluteDistance { get; set; }
        public List<int> List { get; set; }

        public override string ToString()
        {
            return $"{AbsoluteDistance}: " + string.Join(" => ", List);
        }

    }
}
