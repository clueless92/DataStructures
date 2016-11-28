namespace Pr02CountSymbols
{
    using System;
    using System.Linq;
    using Pr01DictionaryImplementation;

    class Program
    {
        static void Main(string[] args)
        {
            LinkedHashMap<char, int> charMap = new LinkedHashMap<char,int>();
            string input = Console.ReadLine();
            foreach (char c in input)
            {
                if (!charMap.ContainsKey(c))
                {
                    charMap.Add(c, 0);
                }

                charMap[c]++;
            }

            foreach (Entry<char, int> entry in charMap.OrderBy(pair => pair.Key))
            {
                Console.WriteLine("{0}: {1} time/s", entry.Key, entry.Value);
            }
        }
    }
}
