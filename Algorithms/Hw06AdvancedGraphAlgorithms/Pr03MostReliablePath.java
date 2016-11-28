package Hw06AdvancedGraphAlgorithms;

import java.util.*;

public class Pr03MostReliablePath {
    private static LinkedHashMap<Node, LinkedHashMap<Node, Double>> graph;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int nodeCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        Node[] nodes = new Node[nodeCount];
        for (int i = 0; i < nodeCount; i++) {
            nodes[i] = new Node(i);
        }
        String[] pathInfo = sc.nextLine().split(" ");
        int startNodeId = Integer.parseInt(pathInfo[1]);
        int finishNodeId = Integer.parseInt(pathInfo[3]);
        int edgeCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        graph = new LinkedHashMap<>();
        for (int i = 0; i < edgeCount; i++) {
            String[] info = sc.nextLine().split(" ");
            Node node1 = nodes[Integer.parseInt(info[0])];
            Node node2 = nodes[Integer.parseInt(info[1])];
            double percentage = Double.parseDouble(info[2]) / 100d;
            if(!graph.containsKey(node1)) {
                graph.put(node1, new LinkedHashMap<>());
            }
            graph.get(node1).put(node2, percentage);
            if(!graph.containsKey(node2)) {
                graph.put(node2, new LinkedHashMap<>());
            }
            graph.get(node2).put(node1, percentage);
        }
        List<Integer> path = findSafestPath(nodes[startNodeId], nodes[finishNodeId], nodeCount);
        System.out.printf("Most reliable path reliability: %.2f%%%n", nodes[finishNodeId].getPercentFromStart() * 100d);
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

    // Dijkstra with maxPriorityQueue (heap) and multiplication of weights
    public static List<Integer> findSafestPath(Node sourceNode, Node destinationNode, int nodeCount) {
        Integer[] prev = new Integer[nodeCount];
        boolean[] visited = new boolean[nodeCount];
        PriorityQueueMax<Node> priorityQueue = new PriorityQueueMax<>();
        for (Node node : graph.keySet()) {
            node.setPercentFromStart(Double.NEGATIVE_INFINITY);
        }
        visited[sourceNode.getId()] = true;
        sourceNode.setPercentFromStart(1.0d);
        priorityQueue.enqueue(sourceNode);
        while (priorityQueue.size() > 0) {
            Node currNode = priorityQueue.extractMax();
            if (currNode.equals(destinationNode)) {
                break;
            }
            for (Node child : graph.get(currNode).keySet()) {
                if (!visited[child.getId()]) {
                    priorityQueue.enqueue(child);
                    visited[child.getId()] = true;
                }
                double percent = currNode.getPercentFromStart() * graph.get(currNode).get(child);
                if (percent > child.getPercentFromStart()) {
                    child.setPercentFromStart(percent);
                    prev[child.getId()] = currNode.getId();
                    priorityQueue.increaseKey(child);
                }
            }
        }
        if (Double.isInfinite(destinationNode.getPercentFromStart())) {
            return null;
        }
        List<Integer> path = new ArrayList<>();
        Integer current = destinationNode.getId();
        while (current != null) {
            path.add(current);
            current = prev[current];
        }
        Collections.reverse(path);
        return path;
    }
}
