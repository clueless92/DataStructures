namespace Pr03FastSearchStringsInTextFile
{
    using System;
    using System.Text;

    class Program
    {
        /// <summary>
        /// Char by char solution
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int countOfSubstrings = int.Parse(Console.ReadLine());
            string[] substrings = new string[countOfSubstrings];
            int[] substringOccurances = new int[countOfSubstrings];
            for (int i = 0; i < countOfSubstrings; i++)
            {
                substrings[i] = Console.ReadLine();
            }

            int countOfSearchLines = int.Parse(Console.ReadLine());
            for (int i = 0; i < countOfSearchLines; i++)
            {
                string line = Console.ReadLine();
                StringBuilder buffer = new StringBuilder();
                foreach (char c in line)
                {
                    buffer.Append(c);
                    for (int k = 0; k < countOfSubstrings; k++)
                    {
                        if (buffer.ToString().EndsWith(substrings[k], StringComparison.InvariantCultureIgnoreCase))
                        {
                            substringOccurances[k]++;
                        }
                    }
                }
            }

            for (int i = 0; i < countOfSubstrings; i++)
            {
                Console.WriteLine("{0} -> {1}", substrings[i], substringOccurances[i]);
            }
        }
    }
}
