using System;
using System.Collections.Generic;

namespace Pr02RoundDance
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            int edgeCount = int.Parse(Console.ReadLine());
            int startNode = int.Parse(Console.ReadLine());

            for (int i = 0; i < edgeCount; i++)
            {
                string[] edge = Console.ReadLine().Split();
                int parentNode = int.Parse(edge[0]);
                int childNode = int.Parse(edge[1]);

                if (!graph.ContainsKey(parentNode))
                {
                    graph.Add(parentNode, new List<int>());
                }

                if (!graph.ContainsKey(childNode))
                {
                    graph.Add(childNode, new List<int>());
                }

                graph[parentNode].Add(childNode);
                graph[childNode].Add(parentNode);
            }

            int max = DFS(graph, startNode);
            Console.WriteLine(max);
        }

        private static int DFS(Dictionary<int, List<int>> graph, int startNode)
        {
            Stack<int> stack = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();
            stack.Push(startNode);
            visited.Add(startNode);

            int maxCandidate = 0;
            int max = 0;
            while (stack.Count > 0)
            {
                int currNode = stack.Pop();
                visited.Add(currNode);
                for (int i = 0; i < graph[currNode].Count; i++)
                {
                    if (!visited.Contains(graph[currNode][i]))
                    {
                        stack.Push(graph[currNode][i]);
                    }
                }

                maxCandidate++;
                if (graph[currNode].Count == 1)
                {
                    if (maxCandidate > max)
                    {
                        max = maxCandidate;
                    }
                    maxCandidate = 0;
                }
            }

            return max;
        }
    }
}
