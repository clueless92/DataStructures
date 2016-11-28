namespace Pr07ImplementALinkedList
{
    using System;
    using System.Runtime.InteropServices;

    public class Program
    {
        private static void Main(string[] args)
        {
            SinglyLinkedList<int> test = new SinglyLinkedList<int>();
            test.Add(1);
            test.Add(2);
            test.Add(3);
            test.Add(4);
            test.Add(3);
            Console.WriteLine(string.Join(" ", test));

            Console.WriteLine(test.FirstIndexOf(3));
            Console.WriteLine(test.LastIndexOf(3));

            test.Remove(0);
            test.Remove(2);
            Console.WriteLine(string.Join(" ", test));
        }
    }
}
