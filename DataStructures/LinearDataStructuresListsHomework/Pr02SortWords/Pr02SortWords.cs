namespace Pr02SortWords
{
    using System;
    using System.Collections.Generic;

    public class Pr02SortWords
    {
        private static void Main(string[] args)
        {
            string[] inputStrings = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> storageList = new List<string>(inputStrings);
            storageList.Sort();
            Console.WriteLine(String.Join(" ", storageList));
        }
    }
}
