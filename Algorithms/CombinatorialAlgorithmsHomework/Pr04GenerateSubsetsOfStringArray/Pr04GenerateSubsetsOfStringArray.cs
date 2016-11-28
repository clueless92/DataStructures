namespace Pr04GenerateSubsetsOfStringArray
{
    using System;

    class Pr04GenerateSubsetsOfStringArray
    {
        static int k;
        static int wordsLength;
        static string[] words;
        static string[] wordsK;

        static void Main()
        {
            char[] splitters = { '{', '}', ' ', ',' };
            Console.Write("s = ");
            words = Console.ReadLine().Split(splitters, StringSplitOptions.RemoveEmptyEntries);
            Console.Write("k = ");
            k = int.Parse(Console.ReadLine());
            wordsK = new string[k];
            wordsLength = words.Length;
            CombinationsWithNoRepetition(0, 0);
        }

        static void CombinationsWithNoRepetition(int index, int start)
        {
            if (index >= k)
            {
                Print(wordsK);
            }
            else
            {
                for (int i = start; i < wordsLength; i++)
                {
                    wordsK[index] = words[i];
                    CombinationsWithNoRepetition(index + 1, i + 1);
                }
            }
        }

        static void Print(string[] arr)
        {
            Console.WriteLine("({0})", string.Join(" ", arr));
        }
    }
}