namespace Pr04LongestPathInTree
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, List<int>> tree = new Dictionary<int, List<int>>();
            int nodeCount = int.Parse(Console.ReadLine());
            Dictionary<int, bool> isRoot = new Dictionary<int, bool>();

            int edgeCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < edgeCount; i++)
            {
                string[] edge = Console.ReadLine().Split();
                int parentNode = int.Parse(edge[0]);
                int childNode = int.Parse(edge[1]);

                if (!tree.ContainsKey(parentNode))
                {
                    tree.Add(parentNode, new List<int>());
                }

                if (!isRoot.ContainsKey(parentNode))
                {
                    isRoot.Add(parentNode, true);
                }

                tree[parentNode].Add(childNode);
                isRoot[childNode] = false;
            }

            int root = 0;
            foreach (KeyValuePair<int, bool> pair in isRoot)
            {
                if (pair.Value)
                {
                    root = pair.Key;
                    break;
                }
            }

            Dictionary<int, HashSet<int>> parents = new Dictionary<int, HashSet<int>>();
            Dictionary<int, int> sumsToRoot = new Dictionary<int, int>();
            StudyTree(tree, root, sumsToRoot, parents);
            int maxPath = int.MinValue;

            foreach (int leaf1 in parents.Keys)
            {
                foreach (int leaf2 in parents.Keys)
                {
                    if (leaf1 == leaf2)
                    {
                        continue;
                    }

                    foreach (int parent in parents[leaf1])
                    {
                        if (parents[leaf2].Contains(parent))
                        {
                            int path1 = sumsToRoot[leaf1] - sumsToRoot[parent];
                            int path2 = sumsToRoot[leaf2] - sumsToRoot[parent];
                            int pathTotal = path1 + path2 + parent;
                            if (pathTotal > maxPath)
                            {
                                maxPath = pathTotal;
                            }

                            break;
                        }
                    }
                }
            }

            Console.WriteLine(maxPath);
        }

        private static void StudyTree(
            Dictionary<int, List<int>> tree,
            int root,
            Dictionary<int, int> sumsToRoot,
            Dictionary<int, HashSet<int>> parents)
        {
            sumsToRoot[root] = root;
            Stack<int> nodeStack = new Stack<int>();
            Stack<int> indexStack = new Stack<int>();
            int currNode = root;
            int currIndex = 0;
            List<int> currChildren = tree.ContainsKey(currNode) ? tree[currNode] : null;
            while (true)
            {
                if (currChildren != null)
                {
                    if (currIndex < currChildren.Count)
                    {
                        nodeStack.Push(currNode);
                        indexStack.Push(currIndex);
                        int currChild = currChildren[currIndex];
                        sumsToRoot[currChild] = currChild + sumsToRoot[currNode];
                        currNode = currChild;
                        currIndex = 0;
                        currChildren = tree.ContainsKey(currNode) ? tree[currNode] : null;
                        continue;
                    }
                }
                else
                {
                    foreach (int element in nodeStack)
                    {
                        if (!parents.ContainsKey(currNode))
                        {
                            parents.Add(currNode, new HashSet<int>());
                        }

                        parents[currNode].Add(element);
                    }
                }

                if (nodeStack.Count > 0)
                {
                    currNode = nodeStack.Pop();
                    currIndex = indexStack.Pop() + 1;
                    currChildren = tree.ContainsKey(currNode) ? tree[currNode] : null;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
