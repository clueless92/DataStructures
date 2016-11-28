package Hw06AdvancedGraphAlgorithms;

import java.util.*;
import java.util.stream.Collectors;

public class Pr02ModifiedKruskalAlgorithm {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int nodeCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        int edgeCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        List<Edge> edges = new ArrayList<>();
        for (int i = 0; i < edgeCount; i++) {
            String[] info = sc.nextLine().split(" ");
            int node1 = Integer.parseInt(info[0]);
            int node2 = Integer.parseInt(info[1]);
            int weight = Integer.parseInt(info[2]);
            edges.add(new Edge(node1, node2, weight));
        }
        List<Edge> resultSpanTree = kruskal(nodeCount, edges);
        printMinSpanTree(resultSpanTree);
    }

    private static void printMinSpanTree(List<Edge> resultSpanTree) {
        int forestWeight = resultSpanTree.stream().collect(Collectors.summingInt(Edge::getDistance));
        System.out.printf("Minimum spanning forest weight: %d%n", forestWeight);
        for (Edge edge : resultSpanTree) {
            System.out.printf("(%d %d) -> %d%n", edge.getStartNode(), edge.getEndNode(), edge.getDistance());
        }
    }

    public static List<Edge> kruskal(int nodeCount, List<Edge> edges) {
        edges.sort(Comparator.<Edge>naturalOrder());
        int[] parent = new int[nodeCount];
        for (int i = 0; i < nodeCount; i++) {
            parent[i] = i;
        }
        List<Edge> minSpanTree = new ArrayList<>();
        for (Edge edge : edges) {
            int rootStartNode = findRoot(edge.getStartNode(), parent);
            int rootEndNode = findRoot(edge.getEndNode(), parent);
            if (rootStartNode == rootEndNode) {
                continue;
            }
            minSpanTree.add(edge);
            parent[rootEndNode] = rootStartNode;
        }
        return minSpanTree;
    }

    public static int findRoot(int node, int[] parent) {
        int root = parent[node];
        while (parent[root] != root) {
            root = parent[root];
        }
        while (node != root) {
            int prevParent = parent[node];
            parent[node] = root;
            node = prevParent;
        }
        return root;
    }
}
