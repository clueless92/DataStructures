namespace Longest_Increasing_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestIncreasingSubsequence
    {
        public static void Main()
        {
            var sequence = new[] { 24, 5, 31, 3, 3, 342, 51, 114, 52, 55, 56, 58 };
//          var sequence = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var longestSeq = FindLongestIncreasingSubsequence(sequence);
            Console.WriteLine("Longest increasing subsequence (LIS)");
            Console.WriteLine("  Length: {0}", longestSeq.Length);
            Console.WriteLine("  Sequence: [{0}]", string.Join(", ", longestSeq));
        }

        public static int[] FindLongestIncreasingSubsequence(int[] sequence)
        {
            int[] len = new int[sequence.Length];
            int[] prev = new int[sequence.Length];
            int maxLen = 0;
            int lastIndex = -1;

            int comparer = -1;
            for (int curr = 0; curr < sequence.Length; curr++)
            {
                len[curr] = 1;
                prev[curr] = -1;
                for (int i = 0; i < curr; i++)
                {
                    if (sequence[i].CompareTo(sequence[curr]) == -1 && len[i] >= len[curr])
                    {
                        len[curr] = len[i] + 1;
                        prev[curr] = i;
                        comparer *= -1;
                    }
                }

                if (len[curr] > maxLen)
                {
                    maxLen = len[curr];
                    lastIndex = curr;
                }
            }

            List<int> longestSeq = new List<int>();
            while (lastIndex != -1)
            {
                longestSeq.Add(sequence[lastIndex]);
                lastIndex = prev[lastIndex];
            }

            longestSeq.Reverse();

            return longestSeq.ToArray();
        }
    }
}
