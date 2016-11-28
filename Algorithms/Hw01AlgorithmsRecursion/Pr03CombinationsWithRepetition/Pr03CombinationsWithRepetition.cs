using System;

namespace Pr03CombinationsWithRepetition
{
    class Pr03CombinationsWithRepetition
    {
        static int k;
        static int n;
        static int[] arr;

        static void Main()
        {
            Console.Write("n=");
            n = int.Parse(Console.ReadLine());
            Console.Write("k=");
            k = int.Parse(Console.ReadLine());
            arr = new int[k];
            CombinationsWithRepetition(0, 1);
        }

        static void CombinationsWithRepetition(int index, int start)
        {
            if (index >= k)
            {
                Console.WriteLine(string.Join(" ", arr));
            }
            else
            {
                for (int i = start; i <= n; i++)
                {
                    arr[index] = i;
                    CombinationsWithRepetition(index + 1, i);
                }
            }
        }
    }
}
