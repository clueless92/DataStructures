package Hw06AdvancedGraphAlgorithms;

import java.util.Scanner;

public class Pr04ShortestPathsBetweenAllPairsOfNodes {
    private static int[][] graph;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int nodesCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        graph = new int[nodesCount][nodesCount];
        int edgesCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        for (int r = 0; r < graph.length; r++) {
            for (int c = 0; c < graph[r].length; c++) {
                if(r == c) {
                    graph[r][c] = 0;
                } else {
                    graph[r][c] = Integer.MAX_VALUE >> 1;
                }
            }
        }
        for (int i = 0; i < edgesCount; i++) {
            String[] info = sc.nextLine().split(" ");
            int start = Integer.parseInt(info[0]);
            int end = Integer.parseInt(info[1]);
            int dist = Integer.parseInt(info[2]);
            graph[start][end] = dist;
            graph[end][start] = dist;
        }
        findAllShortestPaths();
        printOutput();
    }

    // classic Floyd-Warshall
    private static void findAllShortestPaths() {
        for (int k = 0; k < graph.length; k++) {
            for (int i = 0; i < graph.length; i++) {
                for (int j = 0; j < graph.length; j++) {
                    if (graph[i][k] + graph[k][j] < graph[i][j]) {
                        graph[i][j] = graph[i][k] + graph[k][j];
                    }
                }
            }
        }
    }

    private static void printOutput() {
        System.out.println("Shortest paths matrix:");
        for (int i = 0; i < graph.length; i++) {
            if (i == 0) {
                System.out.printf("%2d", i);
            } else {
                System.out.printf("%3d", i);
            }
        }

        String dashes = new String(new char[graph.length * 3 - 1]).replace('\0', '-');
        System.out.printf("%n%s%n", dashes);

        for (int r = 0; r < graph.length; r++) {
            for (int c = 0; c < graph[r].length; c++) {
                if (c == 0) {
                    System.out.printf("%2d", graph[r][c]);
                } else {
                    System.out.printf("%3d", graph[r][c]);
                }
            }

            System.out.println();
        }
    }
}
