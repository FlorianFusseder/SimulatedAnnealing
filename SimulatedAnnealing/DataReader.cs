using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimulatedAnnealing
{
    internal static class DataReader
    {
        private static string line = string.Empty;
        internal static TourManager ReadTSP(string v)
        {
            var list = new List<Tuple<int, int>>();

            using (var stream = File.Open(v, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {

                    for (int i = 0; i < 6; i++)
                    {
                        reader.ReadLine();
                    }

                    while ((line = reader.ReadLine()) != "EOF")
                    {
                        var i = line.Split()
                            .Where(c => !string.IsNullOrEmpty(c));


                        try
                        {
                            int x = Int32.Parse(i.ElementAt(1));
                            int y = Int32.Parse(i.ElementAt(2));
                            list.Add(new Tuple<int, int>(x, y));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Could not parse at line {i.ElementAt(0)}. Data was {i.ElementAt(1)}, {i.ElementAt(2)}");
                        }                        
                    }
                }
            }

            return new TourManager(list);
        }
    }
}