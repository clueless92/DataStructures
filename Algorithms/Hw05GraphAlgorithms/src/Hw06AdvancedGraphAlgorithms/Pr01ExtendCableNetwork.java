package Hw06AdvancedGraphAlgorithms;

import java.util.ArrayList;
import java.util.Scanner;

public class Pr01ExtendCableNetwork {
    private static int[][] distances;
    private static ArrayList<Integer>[] nodes;
    private static boolean[] connected;
    private static PriorityQueueMin<Edge> priorityQueue;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int budget = Integer.parseInt(sc.nextLine().split(" ")[1]);
        int nodesCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        int edgesCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        distances = new int[nodesCount][nodesCount];
        nodes = new ArrayList[nodesCount];
        connected = new boolean[nodesCount];
        priorityQueue = new PriorityQueueMin<>();
        for (int i = 0; i < nodesCount; i++) {
            nodes[i] = new ArrayList<>();
        }
        for (int i = 0; i < edgesCount; i++) {
            String[] info = sc.nextLine().split(" ");
            int node1 = Integer.parseInt(info[0]);
            int node2 = Integer.parseInt(info[1]);
            int distance = Integer.parseInt(info[2]);
            distances[node1][node2] = distance;
            distances[node2][node1] = distance;
            nodes[node1].add(node2);
            nodes[node2].add(node1);
            if (info.length > 3) { // connected check
                connected[node1] = true;
                connected[node2] = true;
            }
        }
        spendBudgetOptimally(budget, nodesCount);
    }

    // Prim with some pre-connected nodes
    private static void spendBudgetOptimally(int budget, int nodesCount) {
        for (int node = 0; node < nodesCount; node++) {
            if (connected[node]) {
                for (Integer child : nodes[node]) {
                    if(!connected[child]) {
                        priorityQueue.enqueue(new Edge(node, child, distances[node][child]));
                    }
                }
            }
        }

        int startBudget = budget;
        while (priorityQueue.size() > 0) {
            Edge currEdge = priorityQueue.extractMin();
            int currNode = currEdge.getEndNode();
            if (budget - currEdge.getDistance() < 0) {
                break;
            }

            if(connected[currNode]) {
                continue;
            }

            budget -= currEdge.getDistance();
            connected[currNode] = true;
            for (Integer child : nodes[currNode]) {
                if (!connected[child]) {
                    priorityQueue.enqueue(new Edge(currNode, child, distances[currNode][child]));
                }
            }

            System.out.println(currEdge);
        }

        System.out.printf("Budget used: %d%n", startBudget - budget);
    }
}
