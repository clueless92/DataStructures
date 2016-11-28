namespace Pr01FindTheRoot
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int nodeCount = int.Parse(Console.ReadLine());
            bool[] hasParent = new bool[nodeCount];
            int edgeCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < edgeCount; i++)
            {
                string[] line = Console.ReadLine().Split();
                int chidlNode = int.Parse(line[1]);
                hasParent[chidlNode] = true;
            }

            int rootNode = int.MinValue;
            int rootCount = 0;
            for(int i = 0; i < nodeCount; i++)
            {
                if (!hasParent[i])
                {
                    rootCount++;
                    rootNode = i;
                }
            }

            if (rootCount == 0)
            {
                Console.WriteLine("No root!");
            }
            else if(rootCount > 1)
            {
                Console.WriteLine("Multiple root nodes!");
            }
            else
            {
                Console.WriteLine(rootNode);   
            }
        }
    }
}
