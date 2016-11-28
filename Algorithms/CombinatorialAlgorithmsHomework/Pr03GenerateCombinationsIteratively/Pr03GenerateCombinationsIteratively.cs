namespace Pr03GenerateCombinationsIteratively
{
    using System;

    class Pr03GenerateCombinationsIteratively
    {
        static void Main(string[] args)
        {
            Console.Write("n=");
            int n = int.Parse(Console.ReadLine());
            Console.Write("k=");
            int k = int.Parse(Console.ReadLine());
            int[] arr = new int[k];
            for (int i = 0; i < k; i++)
            {
                arr[i] = i;
            }
            GenerateCombosIteratively(n, k, arr);
        }

        static void GenerateCombosIteratively(int n, int k, int[] arr)
        {
            while (arr[k - 1] < n)
            {
                for (int i = 0; i < k; i++)
                {
                    Console.Write("{0} ", arr[i] + 1);
                }
                Console.WriteLine();

                int t = k - 1;
                while (t != 0 && arr[t] == n - k + t)
                {
                    t--;
                }
                arr[t]++;
                for (int i = t + 1; i < k; i++)
                {
                    arr[i] = arr[i - 1] + 1;
                }
            }
        }
    }
}