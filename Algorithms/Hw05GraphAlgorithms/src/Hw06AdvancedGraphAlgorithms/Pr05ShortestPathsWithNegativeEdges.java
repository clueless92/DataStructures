package Hw06AdvancedGraphAlgorithms;

import java.util.*;

public class Pr05ShortestPathsWithNegativeEdges {
    private static List<Edge> edges;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int nodeCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        String[] pathInfo = sc.nextLine().split(" ");
        int startNodeId = Integer.parseInt(pathInfo[1]);
        int finishNodeId = Integer.parseInt(pathInfo[3]);
        int edgeCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        edges = new ArrayList<>();
        for (int i = 0; i < edgeCount; i++) {
            String[] info = sc.nextLine().split(" ");
            int start = Integer.parseInt(info[0]);
            int end = Integer.parseInt(info[1]);
            int weight = Integer.parseInt(info[2]);
            edges.add(new Edge(start, end, weight));
        }
        List<Integer> path = findBestPath(startNodeId, finishNodeId, nodeCount);
        printPath(path);
    }

    private static void printPath(List<Integer> path) {
        for (int i = 0; i < path.size(); i++) {
            if (i == 0) {
                System.out.print(path.get(i));
            } else {
                System.out.printf(" -> %d", path.get(i));
            }
        }
        System.out.println();
    }

    private static List<Integer> findBestPath(int source, int destination, int nodeCount) {
        int[] distance = new int[nodeCount];
        Integer[] prev = new Integer[nodeCount];
        for (int i = 0; i < nodeCount; i++) {
            distance[i] = Integer.MAX_VALUE >> 1;
        }
        distance[source] = 0;
        for (int i = 0; i < nodeCount - 1; i++) {
            for (Edge edge : edges) {
                int start = edge.getStartNode();
                int end = edge.getEndNode();
                int weight = edge.getDistance();
                if (distance[start] + weight < distance[end]) {
                    distance[end] = distance[start] + weight;
                    prev[end] = start;
                }
            }
        }
        // check for negative weight cycles
        Integer cycleEnd = null;
        for (Edge edge : edges) {
            int start = edge.getStartNode();
            int end = edge.getEndNode();
            int weight = edge.getDistance();
            if (distance[start] + weight < distance[end]) {
                cycleEnd = start; // cycle detected, save cycle endNode
            }
        }
        // reconstruct path
        List<Integer> path = new ArrayList<>();
        if (cycleEnd == null) { // if no cycle detected
            Integer current = destination;
            while (current != null) {
                path.add(current);
                current = prev[current];
            }
            System.out.printf("Distance [%d -> %d]: %d%n", source, destination, distance[destination]);
            System.out.print("Path: ");
        } else { // if cycle detected
            Integer current = cycleEnd;
            do {
                path.add(current);
                current = prev[current];
            } while (!Objects.equals(current, cycleEnd));
            System.out.print("Negative cycle detected: ");
        }
        Collections.reverse(path);
        return path;
    }
}
