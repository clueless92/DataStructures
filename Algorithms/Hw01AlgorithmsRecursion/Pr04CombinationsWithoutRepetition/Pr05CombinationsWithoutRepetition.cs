namespace Pr04CombinationsWithoutRepetition
{
    using System;

    class Pr05CombinationsWithoutRepetition
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
            CombinationsWithNoRepetition(0, 1);
        }

        static void CombinationsWithNoRepetition(int index, int start)
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
                    CombinationsWithNoRepetition(index + 1, i + 1);
                }
            }
        }
    }
}
