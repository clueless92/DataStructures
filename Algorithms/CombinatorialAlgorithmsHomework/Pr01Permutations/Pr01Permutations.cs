namespace Pr01Permutations
{
    using System;
    using System.Linq;

    class Pr01Permutations
    {
        static int permutationsCount = 0;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Enumerable.Range(1, n).ToArray();
            Permute(arr);
            Console.WriteLine("Total permutations: {0}", permutationsCount);
        }

        static void Permute(int[] arr, int start = 0)
        {
            int size = arr.Length;
            if (start >= size - 1)
            {
                Console.WriteLine(string.Join(", ", arr));
                permutationsCount++;
            }
            else
            {
                for (int i = start; i < size; i++)
                {
                    swap(ref arr[start], ref arr[i]);
                    Permute(arr, start + 1);
                    swap(ref arr[start], ref arr[i]);
                }
            }
        }

        static void swap(ref int one, ref int other)
        {
            if (one == other)
            {
                return;
            }
            int swaper = one;
            one = other;
            other = swaper;
        }
    }
}