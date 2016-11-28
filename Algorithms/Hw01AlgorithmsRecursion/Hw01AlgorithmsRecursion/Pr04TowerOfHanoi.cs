namespace Pr01TowerOfHanoi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Pr04TowerOfHanoi
    {
        static int movesMade = 0;
        static Stack<int> sourceRod;
        static readonly Stack<int> destinationRod = new Stack<int>();
        static readonly Stack<int> spareRod = new Stack<int>();

        static void Main(string[] args)
        {
            int bottomDisk = int.Parse(Console.ReadLine());
            sourceRod = new Stack<int>(Enumerable.Range(1, 3).Reverse());
            PrintRods();
            MoveDisks(bottomDisk, sourceRod, destinationRod, spareRod);
        }

        static void MoveDisks(int bottomDisk, Stack<int> source, Stack<int> destination, Stack<int> spare)
        {
            if (bottomDisk == 1)
            {
                movesMade++;
                destination.Push(source.Pop());
                Console.WriteLine("Step #{0}: Moved disk {1}", movesMade, bottomDisk);
                PrintRods();
            }
            else
            {
                MoveDisks(bottomDisk - 1, source, spare, destination);
                movesMade++;
                destination.Push(source.Pop());
                Console.WriteLine("Step #{0}: Moved disk {1}", movesMade, bottomDisk);
                PrintRods();
                MoveDisks(bottomDisk - 1, spare, destination, source);
            }
        }

        static void PrintRods()
        {
            Console.WriteLine("Source: {0}", string.Join(", ", sourceRod.Reverse()));
            Console.WriteLine("Destination: {0}", string.Join(", ", destinationRod.Reverse()));
            Console.WriteLine("Spare: {0}", string.Join(", ", spareRod.Reverse()));
            Console.WriteLine();
        }
    }
}
