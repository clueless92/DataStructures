namespace Pr05CountOfOccurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Pr05CountOfOccurrences
    {
        private static void Main(string[] args)
        {
            List<int> inputList = Console.ReadLine().Split().Select(int.Parse).ToList();
            IDictionary<int, int> occurancesTracker = new SortedDictionary<int, int>();
            foreach (var num in inputList)
            {
                if (!occurancesTracker.ContainsKey(num))
                {
                    occurancesTracker.Add(num, 0);
                }

                occurancesTracker[num]++;
            }

            foreach (var pair in occurancesTracker)
            {
                Console.WriteLine("{0} -> {1} times", pair.Key, pair.Value);
            }
        }
    }
}
