namespace Pr0RemoveOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Pr04RemoveOddOccurences
    {
        private static void Main(string[] args)
        {
            List<int> inputList = Console.ReadLine().Split().Select(int.Parse).ToList();
            IDictionary<int, int> occurancesTracker = new Dictionary<int, int>();
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
                if (pair.Value % 2 != 0)
                {
                    inputList.RemoveAll(num => num == pair.Key);
                }
            }

            Console.WriteLine(string.Join(" ", inputList));
        }
    }
}
