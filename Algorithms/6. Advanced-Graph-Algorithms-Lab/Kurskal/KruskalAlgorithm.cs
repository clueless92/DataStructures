namespace Kurskal
{
    using System.Collections.Generic;

    public class KruskalAlgorithm
    {
        public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
        {
            edges.Sort();
            int[] parent = new int[numberOfVertices];
            for (int i = 0; i < numberOfVertices; i++)
            {
                parent[i] = i;
            }

            List<Edge> minSpanTree = new List<Edge>();
            foreach (Edge edge in edges)
            {
                int rootStartNode = FindRoot(edge.StartNode, parent);
                int rootEndNode = FindRoot(edge.EndNode, parent);
                if (rootStartNode == rootEndNode)
                {
                    continue;
                }

                minSpanTree.Add(edge);
                parent[rootEndNode] = rootStartNode;
            }

            return minSpanTree;
        }

        public static int FindRoot(int node, int[] parent)
        {
            int root = parent[node];
            while (parent[root] != root)
            {
                root = parent[root];
            }

            while (node != root)
            {
                int prevParent = parent[node];
                parent[node] = root;
                node = prevParent;
            }

            return root;
        }
    }
}
