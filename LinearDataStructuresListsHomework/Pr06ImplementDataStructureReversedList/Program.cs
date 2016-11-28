namespace Pr06ImplementDataStructureReversedList
{
    using System;

    public class Program
    {
        private static void Main(string[] args)
        {
            ReversedList<int> reversedList = new ReversedList<int>(5);

            Console.WriteLine("Test Add without resize:");
            reversedList.Add(1);
            reversedList.Add(2);
            reversedList.Add(3);
            reversedList.Add(4);
            reversedList.Add(5);
            Console.WriteLine(string.Join(" ", reversedList));
            Console.WriteLine("Count: " + reversedList.Count);
            Console.WriteLine("Capacity: " + reversedList.Capacity);
            Console.WriteLine();

            Console.WriteLine("Test Add with resize:");
            reversedList.Add(6);
            Console.WriteLine(string.Join(" ", reversedList));
            Console.WriteLine("Count: " + reversedList.Count);
            Console.WriteLine("Capacity: " + reversedList.Capacity);
            Console.WriteLine();

            Console.WriteLine("Test Remove(3):");
            reversedList.Remove(3);
            Console.WriteLine(string.Join(" ", reversedList));
            Console.WriteLine("Count: " + reversedList.Count);
            Console.WriteLine("Capacity: " + reversedList.Capacity);
            Console.WriteLine();

            Console.WriteLine("Test this[0] set:");
            reversedList[0] = 0;
            Console.WriteLine(string.Join(" ", reversedList));
            Console.WriteLine("Count: " + reversedList.Count);
            Console.WriteLine("Capacity: " + reversedList.Capacity);
            Console.WriteLine();

            Console.WriteLine("Test this[1] get:");
            Console.WriteLine(reversedList[1]);
        }
    }
}
