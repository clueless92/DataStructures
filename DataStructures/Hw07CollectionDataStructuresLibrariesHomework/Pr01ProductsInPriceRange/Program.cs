namespace Pr01ProductsInPriceRange
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    class Program
    {
        static void Main(string[] args)
        {
            var products = new OrderedMultiDictionary<double, string>(false);

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] entry = Console.ReadLine().Split();
                string productName = entry[0];
                double price = double.Parse(entry[1]);
                products[price].Add(productName);
            }

            string[] borders = Console.ReadLine().Split();
            double min = double.Parse(borders[0]);
            double max = double.Parse(borders[1]);
            int count = 0;
            var subProducts = products.Range(min, true, max, true);

            foreach (KeyValuePair<double, ICollection<string>> pair in subProducts)
            {
                if (pair.Key > max || pair.Key < min)
                {
                    continue;
                }

                foreach (string innerValue in pair.Value)
                {
                    if (count == 20)
                    {
                        return;
                    }

                    Console.WriteLine("{0:f2} {1}", pair.Key, innerValue);
                    count++;
                }
            }
        }
    }
}
