namespace Pr01ReverseArray
{
    using System;

    class Pr01ReverseArray
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            PrintReversed(input, input.Length - 1);
        }

        private static void PrintReversed(string[] input, int index)
        {
            if (index < 0)
            {
                Console.WriteLine();
                return;
            }

            Console.Write("{0} ", input[index]);
            PrintReversed(input, --index);
        }
    }
}
