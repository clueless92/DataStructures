namespace Pr02GeneratePermutationsIteratively
{
    using System;
    using System.Linq;

    class Pr02GeneratePermutationsIteratively
    {
        static int permutationsCount = 0;
        static void Main(string[] args)
        {
            Console.Write("n=");
            int n = int.Parse(Console.ReadLine());
            int[] arr = Enumerable.Range(1, n).ToArray();
            int[] arrHelp = new int[n + 1];
            for (int i = 0; i < arrHelp.Length; i++)
            {
                arrHelp[i] = i;
            }
            PermuteIteratively(arr, arrHelp, n);
            Console.WriteLine("Total permutations: {0}", permutationsCount);
        }

        static void PermuteIteratively(int[] arr, int[] arrHelp, int n)
        {
            int i = 0;
            while (i < n)
            {
                arrHelp[i]--;
                int h;
                if (IsOdd(i))
                {
                    h = arrHelp[i];
                }
                else
                {
                    h = 0;
                }
                swap(ref arr[h], ref arr[i]);
                i = 1;
                while (arrHelp[i] == 0)
                {
                    arrHelp[i] = i;
                    i++;
                }
                Console.WriteLine(string.Join(", ", arr));
                permutationsCount++;
            }
        }

        static bool IsOdd(int i)
        {
            if (i % 2 == 0)
            {
                return false;
            }
            return true;
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