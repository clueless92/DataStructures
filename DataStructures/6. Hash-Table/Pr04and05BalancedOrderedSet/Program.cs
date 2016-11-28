namespace Pr04and05BalancedOrderedSet
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var set = new BalancedOrderedSet<int>();
            set.Add(17);
            set.Add(9);
            set.Add(12);
            set.Add(19);
            set.Add(6);
            set.Add(25);
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Count: " + set.Count);
            Console.WriteLine("Contains(12): " + set.Contains(12));

            Console.WriteLine("Remove(12): " + set.Remove(12));
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Count: " + set.Count);
            Console.WriteLine("Contains(12): " + set.Contains(12));

            Console.WriteLine("Remove(12): " + set.Remove(12));
            Console.WriteLine("Count: " + set.Count);

            set.Clear();
            Console.WriteLine("Count: " + set.Count);
            try
            {
                foreach (var item in set)
                {
                    Console.WriteLine(item);
                }
            }
            catch (NullReferenceException nre)
            {
                Console.WriteLine(nre.Message);
            }
        }
    }
}
