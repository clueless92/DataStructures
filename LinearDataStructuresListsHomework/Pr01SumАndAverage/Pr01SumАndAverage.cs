namespace Pr01SumАndAverage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Pr01SumАndAverage
    {
        private static void Main(string[] args)
        {
            string[] inputStrings = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> storageList = new List<int>();
            foreach (string numString in inputStrings)
            {
                storageList.Add(int.Parse(numString));
            }

            int sum = 0;
            double avg = 0d;
            if (storageList.Count != 0)
            {
                sum = storageList.Sum();
                avg = storageList.Average();
            }
            Console.WriteLine("Sum={0}; Average={1}", sum, avg);
        }
    }
}
