namespace Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GraphConnectedComponents
    {
        private static bool[] visited;
        private static List<int>[] graph;

        public static void Main()
        {
            graph = ReadGraph();
            FindGraphConnectedComponents();
        }

        private static void FindGraphConnectedComponents()
        {
            visited = new bool[graph.Length];
            for (int startNode = 0; startNode < graph.Length; startNode++)
            {
                if (!visited[startNode])
                {
                    Console.Write("Connected component:");
                    DFS(startNode);
                    Console.WriteLine();
                }
            }
        }

        private static List<int>[] ReadGraph()
        {
            int n = int.Parse(Console.ReadLine());
            var graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                List<int> innerGraph = new List<int>();
                string[] line = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in line)
                {
                    innerGraph.Add(int.Parse(s));
                }

                graph[i] = innerGraph;
            }

            return graph;
        }

        static void DFS(int node)
        {
            if (!visited[node])
            {
                visited[node] = true;
                foreach(var child in graph[node])
                {
                    DFS(child);
                }
                Console.Write(" {0}", node);
            }
        }


    }
}
