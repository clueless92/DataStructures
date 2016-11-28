namespace Pr5PermutationsWithRepetition
{
    using System;
    using System.Linq;

    class Pr5PermutationsWithRepetition
    {
        static int permutationsWithRepCount = 0;
        static int[] arr;
        static void Main(string[] args)
        {
            char[] splitters = { '{', '}', ' ', ',' };
            Console.Write("s = ");
            arr = Console.ReadLine().Split(splitters, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            Array.Sort(arr);
            PermuteWithRep(0, arr.Length);
            //Console.WriteLine("Total permutations: {0}", permutationsWithRepCount);
        }

        private static void PermuteWithRep(int start, int size)
        {
            PrintPerm();
            permutationsWithRepCount++;
            int swap = 0;
            if (start < size)
            {
                for (int i = size - 2; i >= start; i--)
                {
                    for (int k = i + 1; k < size; k++)
                    {
                        if (arr[i] != arr[k])
                        {
                            swap = arr[i];
                            arr[i] = arr[k];
                            arr[k] = swap;
                            PermuteWithRep(i + 1, size);
                        }
                    }
                    // comment the code below to see the difference
                    swap = arr[i];
                    for (int k = i; k < size - 1; )
                    {
                        arr[k] = arr[++k];
                    }
                    arr[size - 1] = swap;
                }
            }
        }

        static void PrintPerm()
        {
            Console.Write("{ ");
            Console.Write(string.Join(", ", arr));
            Console.WriteLine(" }");
        }
    }
}