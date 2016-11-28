using System;

namespace AvlTreeLab
{
    using System.Collections.Generic;

    public class Program
    {
        static void Main()
        {
            var tree = new AvlTree<int>();
            string[] items = Console.ReadLine().Split();
            foreach (string item in items)
            {
                tree.Add(int.Parse(item));
            }

            string[] range = Console.ReadLine().Split();
            int from = int.Parse(range[0]);
            int to = int.Parse(range[1]);

            var subTree = tree.Range(from, to);
            Console.WriteLine(string.Join(" ", subTree)); // implemented Enumerator for AvlTree
        }
    }
}
