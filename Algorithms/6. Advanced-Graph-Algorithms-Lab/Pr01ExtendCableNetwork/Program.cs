namespace Pr01ExtendCableNetwork
{
    using System;
    using Dijkstra;
    using Kurskal;
    using System.Collections.Generic;

    class Program
    {
        static int[][] distances;
        static List<int>[] nodes;
        static bool[] connected;
        static PriorityQueue<Edge> priorityQueue;

        static void Main(string[] args)
        {
            int budget = int.Parse(Console.ReadLine().Split(' ')[1]);
            int nodesCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            int edgesCount = int.Parse(Console.ReadLine().Split(' ')[1]);

            distances = new int[nodesCount][];
            nodes = new List<int>[nodesCount];
            connected = new bool[nodesCount];
            priorityQueue = new PriorityQueue<Edge>();
            for (int i = 0; i < nodesCount; i++)
            {
                nodes[i] = new List<int>();
                distances[i] = new int[nodesCount];
            }

            for (int i = 0; i < edgesCount; i++)
            {
                String[] info = Console.ReadLine().Split(' ');
                int node1 = int.Parse(info[0]);
                int node2 = int.Parse(info[1]);
                int distance = int.Parse(info[2]);

                distances[node1][node2] = distance;
                distances[node2][node1] = distance;
                nodes[node1].Add(node2);
                nodes[node2].Add(node1);
                if (info.Length > 3)
                { // connected check
                    connected[node1] = true;
                    connected[node2] = true;
                }
            }

            for (int node = 0; node < nodesCount; node++)
            {
                if (!connected[node])
                {
                    continue;
                }

                foreach (int child in nodes[node])
                {
                    if (connected[child])
                    {
                        continue;
                    }

                    priorityQueue.Enqueue(new Edge(node, child, distances[node][child]));
                }
            }

            int startBudget = budget;
            while (priorityQueue.Count > 0)
            {
                Edge currEdge = priorityQueue.ExtractMin();
                int currNode = currEdge.EndNode;
                if (budget - currEdge.Weight < 0)
                {
                    break;
                }

                if (connected[currNode])
                {
                    continue;
                }

                budget -= currEdge.Weight;
                connected[currNode] = true;
                foreach (int child in nodes[currNode])
                {
                    if (!connected[child])
                    {
                        priorityQueue.Enqueue(new Edge(currNode, child, distances[currNode][child]));
                    }
                }

                Console.WriteLine(currEdge);
            }

            Console.WriteLine("Budget used: {0}", startBudget - budget);
        }
    }
}
