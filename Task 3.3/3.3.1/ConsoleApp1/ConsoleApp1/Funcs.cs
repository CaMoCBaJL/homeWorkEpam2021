using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Funcs
    {
        static public Func<int[], double> sum = (arr) =>
        {
            double sum = 0;

            foreach (var item in arr)
                sum += item;

            return sum;
        };

        static public Func<int[], double> averageElem = (arr) => arr.Average();

        static public Func<int[], double> mostRecentElem = (arr) =>
        {
            Dictionary<double, int> elems = new Dictionary<double, int>();

            foreach (var item in arr)
            {
                if (elems.ContainsKey(item))
                {
                    for (int i = 0; i < elems.Keys.Count; i++)
                    {
                        if (elems.Keys.ElementAt(i) == item)
                        {
                            elems.Remove(item);

                            elems.Add(item, elems.Values.ElementAt(i) + 1);

                            break;
                        }
                    }
                }
                else
                {
                    elems.Add(item, 1);
                }
            }

            return elems.OrderByDescending(item => item.Value).First().Key;
        };
    }
}
