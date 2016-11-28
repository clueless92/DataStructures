namespace Pr03LongestSubsequence
{
    using System;
    using System.Collections.Generic;

    public class Pr03LongestSubsequence
    {
        private static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            List<int> nums = new List<int>();
            int prevNum = int.Parse(input[0]);
            List<int> longestSeq = new List<int>();
            List<int> candidateSeq = new List<int>();
            candidateSeq.Add(prevNum);
            for (int i = 1; i < input.Length; i++)
            {
                string s = input[i];
                int currNum = int.Parse(s);
                nums.Add(currNum);
                if (candidateSeq.Count > longestSeq.Count)
                {
                    longestSeq = new List<int>(candidateSeq);
                }

                if (currNum == prevNum)
                {
                    candidateSeq.Add(currNum);
                }
                else
                {
                    candidateSeq.Clear();
                    candidateSeq.Add(currNum);
                }

                prevNum = currNum;
            }

            if (candidateSeq.Count > longestSeq.Count)
            {
                longestSeq = new List<int>(candidateSeq);
            }

            Console.WriteLine(string.Join(" ", longestSeq));
        }
    }
}
